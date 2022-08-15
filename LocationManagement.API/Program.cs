using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MOCA.Core;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Settings;
using MOCA.Presistence;
using MOCA.Presistence.Contexts;
using MOCA.Services;
using MOCA.Services.Implementation.DynamicLists;
using MOCA.Services.Implementation.LocationManagment;
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationManagement.Api", Version = "v1" });

    // Enables Swagger Documentation
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

builder.Services.AddCors();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

#region Repositories
// Generic
//builder.Services.AddScoped<IGenericRepositoryAsync_Write, GenericRepositoryAsync_Write>();
//builder.Services.AddScoped<IGenericRepositoryAsync_Read, GenericRepositoryAsync_Read>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//-----------------

//builder.Services.AddTransient<ICountryRepository, CountryRepository>();

#endregion

#region Service Layer

// Shared Services
//Auth Would Be Change 
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddScoped<IReservationsStatusService, ReservationsStatusService>();
builder.Services.AddTransient<IDateTimeService, DateTimeService>();
builder.Services.AddTransient<IUploadImageService, UploadImageService>();
//-------------------------

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ILocationTypeService, LocationTypeService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IIndustryService, IndustryService>();
builder.Services.AddScoped<ILocationBankAccountService, LocationBankAccountService>();
builder.Services.AddScoped<ILocationContactService, LocationContactService>();
builder.Services.AddScoped<ILocationCurrencyService, LocationCurrencyService>();
builder.Services.AddScoped<ILocationFileService, LocationFileService>();
builder.Services.AddScoped<ILocationImageService, LocationImageService>();
builder.Services.AddScoped<ILocationInclusionService, LocationInclusionService>();
builder.Services.AddScoped<ILocationWorkingHourService, LocationWorkingHourService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IServiceFeePaymentsDueDateService, ServiceFeePaymentsDueDateService>();
builder.Services.AddScoped<IFavouriteLocationService, FavouriteLocationService>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IBuildingFloorService, BuildingFloorService>();
builder.Services.AddScoped<IWorkSpaceCategoryService, WorkSpaceCategoryService>();
builder.Services.AddScoped<IWorkSpaceTypeService, WorkSpaceTypeService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<IVenueSetupService, VenueSetupService>();

#endregion


#region Settings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));
#endregion

#region JWT Authentication
//Here would be change whenever beshbeshy added SSo In Moca BackEnd
builder.Services.AddAuthentication(
    options =>
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
#endregion

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocationManagement.Api v1"));

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),

    RequestPath = new PathString("/Resources")

});
app.MapControllers();

app.Run();
