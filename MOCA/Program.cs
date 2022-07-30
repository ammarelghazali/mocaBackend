using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.SSO.Identity;
using MOCA.Core.Interfaces.MocaSettings.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Presistence;
using MOCA.Presistence.Contexts;
using MOCA.Services;
using MOCA.Services.Implementation.MocaSettings;
using MOCA.Services.Implementation.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver =
                                 new DefaultContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MocaSettings.Api", Version = "v1" });

    // Enables Swagger Documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

builder.Services.AddCors();

builder.Services.AddHttpContextAccessor();

// Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Identity
builder.Services.AddIdentity<Admin, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


// Shared Services
builder.Services.AddTransient<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Adds Api Versioning
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

// Enables JWT Authentication
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

// Service Layer
#region Moca Settings Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICaseTypeService, CaseTypeService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IIssueReportService, IssueReportService>();
builder.Services.AddScoped<IPlansService, PlansService>();
builder.Services.AddScoped<IPlanTypesService, PlanTypesService>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IPolicyTypesService, PolicyTypesService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddScoped<ISeveritiesService, SeveritiesService>();
builder.Services.AddScoped<IStatusesService, StatusesService>();
builder.Services.AddScoped<ITopUpsService, TopUpsService>();
builder.Services.AddScoped<ITopUpTypesService, TopUpTypesService>();
builder.Services.AddScoped<IWifisService, WifisService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MocaContentBack.Api v1"));


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();
app.Run();
