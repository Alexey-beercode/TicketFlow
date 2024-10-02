using FluentValidation.AspNetCore;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Token;
using UserService.BLL.Infrastructure.Identity;
using UserService.BLL.Infrastructure.Mapper;
using UserService.BLL.Infrastructure.Validators;
using UserService.BLL.Interfaces;
using UserService.BLL.Services;
using UserService.DLL.Configuration;
using UserService.DLL.Repositories.Implementations;
using UserService.DLL.Repositories.Interfaces;
using UserService.Domain.Entities;
using ITokenService = UserService.BLL.Interfaces.ITokenService;

namespace UserService.Domain.Extensions;

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
            typeof(UserProfile).Assembly,
            typeof(RoleProfile).Assembly,
            typeof(TokenProfile).Assembly
        );
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("ConnectionString");
        builder.Services.AddDbContext<UserDbContext>(options => { options.UseNpgsql(connectionString); });
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<UserManager<User>>();  
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IRoleService,RoleService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IUserService, BLL.Services.UserService>();
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
    }

    public static void AddValidation(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();


    }

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }

    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();

        var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
        var migrationsAssembly = typeof(UserDbContext).Assembly.GetName().Name;

        var config = new IdentityServerConfig(builder.Configuration);
        builder.Services.AddSingleton(config);

        builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.IssuerUri = builder.Configuration["IdentityServer:Authority"];
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseNpgsql(
                    connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseNpgsql(
                    connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddInMemoryClients(config.GetClients())
            .AddInMemoryIdentityResources(config.GetIdentityResources())
            .AddInMemoryApiScopes(config.GetApiScopes())
            .AddDeveloperSigningCredential();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityServer:Authority"];
                options.RequireHttpsMetadata = false;
                options.Audience = builder.Configuration["IdentityServer:Scope"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["IdentityServer:Authority"],
                    ValidAudience = builder.Configuration["IdentityServer:Scope"]
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy("Admin", policy => { policy.RequireRole("Admin"); });
        });

        builder.Services.Configure<IdentityOptions>(options => { options.ClaimsIdentity.UserIdClaimType = "sub"; });

        builder.Services.Configure<IdentityServerOptions>(options =>
        {
            options.Authentication.CookieLifetime = TimeSpan.FromHours(1);
            options.Authentication.CookieSlidingExpiration = true;
        });

    }


}
