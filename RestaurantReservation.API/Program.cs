using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.API.Services;
using RestaurantReservation.API.Validators.Authentication;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.Console()
             .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddDbContext<RestaurantReservationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Databases:ConnectionStrings:SqlServer"]);
});

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<ICustomerRepository, ApplicationRepository>();
builder.Services.AddScoped<IEmployeeRepository, ApplicationRepository>();
builder.Services.AddScoped<IMenuItemRepository, ApplicationRepository>();
builder.Services.AddScoped<IOrderRepository, ApplicationRepository>();
builder.Services.AddScoped<IOrderItemRepository, ApplicationRepository>();
builder.Services.AddScoped<IReservationRepository, ApplicationRepository>();
builder.Services.AddScoped<IRestaurantRepository, ApplicationRepository>();
builder.Services.AddScoped<ITableRepository, ApplicationRepository>();
builder.Services.AddScoped<IUserRepository, ApplicationRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
           Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"] ?? throw new Exception()))
    };
}
);

builder.Services.AddAuthorization();

builder.Services.AddApiVersioning(setup =>
{
    setup.ReportApiVersions = true;
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.DefaultApiVersion = new ApiVersion(1, 0);
}).AddMvc()
.AddApiExplorer(setupAction =>
{
    setupAction.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

var apiVersionDescriptionProviders = builder.Services.BuildServiceProvider()
                                     .GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen(setupAction =>
{
    foreach(var description in apiVersionDescriptionProviders.ApiVersionDescriptions)
    {
        setupAction.SwaggerDoc($"{description.GroupName}", 
        new()
        {
            Title = "Restaurant Reservation API",
            Version = description.ApiVersion.ToString(),
            Description = "Through this API you can access Restaurants Reservations, Customers, Orders, and much more!"
        });
    }

    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.AddSecurityDefinition("RestaurantReservationApiBearerAuth", new()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new()
    {
        {
            new ()
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "RestaurantReservationApiBearerAuth" }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            setupAction.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();