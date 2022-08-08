using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.Shared.Services.ThirdParty.Email;
using MOCA.Core.Settings;
using MOCA.Presistence;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using MOCA.Presistence.Repositories.MeetingSpaceReservations;
using MOCA.Services;
using MOCA.Services.Implementation.MeetingSpaceReservations;
using MOCA.Services.Implementation.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver =
                                 new DefaultContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventSpaces.Api", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version as 1.0
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());



// Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMeetingSpaceReservationRepository, MeetingSpaceReservationsRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IReservationsStatusService, ReservationsStatusService>();
builder.Services.AddScoped<IMeetingSpaceReservationsServices, MeetingSpaceREservationsServices>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Shared Services
builder.Services.AddTransient<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IReservationsStatusService, ReservationsStatusService>();
builder.Services.AddScoped<IMeetingSpaceReservationsServices, MeetingSpaceREservationsServices>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Shared Services
builder.Services.AddTransient<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IReservationsStatusService, ReservationsStatusService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Shared Services
builder.Services.AddTransient<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IReservationsStatusService, ReservationsStatusService>();

// Settings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"]))
        };
        o.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = c =>
            {
                c.Response.OnStarting(async () =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 401;
                    c.Response.ContentType = "text/plain";
                    await c.Response.WriteAsync(c.Exception.ToString());
                });
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.Response.OnStarting(async () =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                    await context.Response.WriteAsync(result);
                });
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                context.Response.OnStarting(async () =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                    await context.Response.WriteAsync(result);
                });
                return Task.CompletedTask;
            },
        };
    });

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events.Api v1"));


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();
app.Run();