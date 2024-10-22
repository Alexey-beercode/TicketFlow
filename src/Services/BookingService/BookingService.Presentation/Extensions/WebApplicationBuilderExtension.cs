using System.Reflection;
using System.Text;
using BookingService.Application.Facades;
using BookingService.Application.Interfaces.Facades;
using BookingService.Application.Mappers;
using BookingService.Application.UseCases.Trip.Create;
using BookingService.Application.UseCases.Trip.GetAll;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Domain.Interfaces.UnitOfWork;
using BookingService.Infrastructure.Configuration;
using BookingService.Infrastructure.Repositories;
using BookingService.Infrastructure.Repositories.Implementations;
using BookingService.Presentation.Validators.Coupon;
using BookingService.Presentation.Validators.Ticket;
using BookingService.Presentation.Validators.Trip;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;

namespace BookingService.Presentation.Extensions;

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
            typeof(CouponProfile).Assembly,
            typeof(SeatTypeProfile).Assembly,
            typeof(TicketProfile).Assembly,
            typeof(TicketStatusProfile).Assembly,
            typeof(TripProfile).Assembly,
            typeof(TripSeatAvailabilityProfile).Assembly,
            typeof(DiscountTypeProfile).Assembly,
            typeof(TripTypeProfile).Assembly
        );
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("ConnectionString");
        builder.Services.AddDbContext<BookingDbContext>(options => { options.UseNpgsql(connectionString); });
        builder.Services.AddScoped<BookingDbContext>();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ICouponRepository, CouponRepository>();
        builder.Services.AddScoped<IDiscountTypeRepository, DiscountTypeRepository>();
        builder.Services.AddScoped<ISeatTypeRepository, SeatTypeRepository>();
        builder.Services.AddScoped<ITicketRepository, TicketRepository>();
        builder.Services.AddScoped<ITicketStatusRepository, TicketStatusRepository>();
        builder.Services.AddScoped<ITripRepository, TripRepository>();
        builder.Services.AddScoped<ITripTypeRepository, TripTypeRepository>();
        builder.Services.AddScoped<ITripSeatAvailabilityRepository, TripSeatAvailabilityRepository>();
        builder.Services.AddScoped<ITicketFacade, TicketFacade>();
        builder.Services.AddScoped<ITripFacade, TripFacade>();
        builder.Services.AddControllers();
    }

    public static void AddValidation(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateCouponCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<GetByCodeQueryValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateTicketCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateTripCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<GetTripsByFilterQueryValidator>();
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

    public static void AddMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(GetAllTripsHandler).Assembly,
            typeof(GetAllTripsQuery).Assembly
        ));
       
    }
}
