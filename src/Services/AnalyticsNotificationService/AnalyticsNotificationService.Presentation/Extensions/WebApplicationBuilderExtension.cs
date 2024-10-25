using System.Text;
using AnalyticsNotificationService.BLL.DTOs.Request.Notification;
using AnalyticsNotificationService.BLL.Interfaces;
using AnalyticsNotificationService.BLL.MappingProfiles;
using AnalyticsNotificationService.BLL.Services;
using AnalyticsNotificationService.DLL.Infrastructure.Database;
using AnalyticsNotificationService.DLL.Infrastructure.Database.Configuration;
using AnalyticsNotificationService.DLL.Interfaces.Repositories;
using AnalyticsNotificationService.DLL.Repositories;
using AnalyticsNotificationService.Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using NLog.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace AnalyticsNotificationService.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddSwaggerDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description = @"Enter JWT Token please.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                }
            );
        });
    }

    public static void AddMapping(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(
            typeof(AnalyticsProfile).Assembly,
            typeof(MetricTypeProfile).Assembly,
            typeof(NotificationProfile).Assembly
        );
    }

    public static void AddMongoDatabase(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection("MongoDbSettings");
        var connectionString = settings.GetValue<string>("ConnectionString");
        var databaseName = settings.GetValue<string>("DatabaseName");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("MongoDB connection string is not configured");
        }

        if (string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentException("MongoDB database name is not configured");
        }

        builder.Services.Configure<NotificationDbSettings>(settings);
    
        builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
        builder.Services.AddScoped<IMongoDatabase>(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });
    
        builder.Services.AddScoped<IMongoCollection<Notification>>(provider =>
        {
            var database = provider.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<Notification>("Notifications");
        });

        builder.Services.AddScoped<IMongoCollection<Analytics>>(provider =>
        {
            var database = provider.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<Analytics>("Analytics");
        });

        builder.Services.AddScoped<IMongoCollection<MetricType>>(provider =>
        {
            var database = provider.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<MetricType>("MetricTypes");
        });

        builder.Services.AddScoped<NotificationDbContext>();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMetricTypeRepository, MetricTypeRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
        builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
        builder.Services.AddScoped<IMetricTypeService, MetricTypeService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        builder.Services.AddControllers();
    }

    public static void AddValidation(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateNotificationDto>();
    }

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }

    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                LogValidationExceptions = true
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy("Admin", policy => { policy.RequireRole("Admin"); });
        });

    }
}
