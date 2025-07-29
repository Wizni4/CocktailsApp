/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using Amazon.CognitoIdentityProvider;

using CocktailsApp.API.Swagger;
using CocktailsApp.Application.Authentication;
using CocktailsApp.Application.Club;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;
using CocktailsApp.Infrastructure.Authentication;

/*
 * Infrastructure namespaces
 */
using CocktailsApp.Infrastructure.ClubAggregate;
using CocktailsApp.Infrastructure.SeedWork;
using CocktailsApp.Infrastructure.UserAggregate;

using FluentValidation;
/*
 * Framework namespaces
 */
using Hellang.Middleware.ProblemDetails;

using MediatR;

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Globalization;


namespace CocktailsApp.API
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
#if LOCAL || LOCAL_COGNITO
            string connectionStr = configuration.GetConnectionString("Local");
#elif AWS_TEST
            string connectionStr = await SecretManager.GetSecret(configuration);
#endif
            services.AddDbContextPool<EFDbContext>(options => options.UseSqlServer(connectionStr));
            return services;
        }

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
#if LOCAL
            services.AddScoped<IAuthService, LocalAuthService>();
#elif LOCAL_COGNITO
            services.AddScoped<IAuthService, CognitoAuthService>();
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonCognitoIdentityProvider>();
            services.Configure<CognitoSettings>(configuration.GetSection("Cognito"));
#endif
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IIncludable<>), typeof(Includable<>));
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Club>, ClubRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            return services;
        }

        public static IServiceCollection AddEventDispatcher(this IServiceCollection services)
        {
            services.AddScoped<DomainEventDispatcher>();
            return services;
        }

        public static IServiceCollection AddMediatR(this IServiceCollection services, IConfiguration configuration)
        {
            var licenceKey = configuration["LuckyPenny:LicenseKey"];
            services.AddMediatR(cfg => {
                cfg.LicenseKey = licenceKey;

                // Auth
                // -- Command Hanlders
                cfg.RegisterServicesFromAssemblyContaining<SignInCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<SignOutCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<SignUpCommandHandler>();

                // Club
                // -- Command Hanlders
                cfg.RegisterServicesFromAssemblyContaining<AddCocktailCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<AddMemberCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<AddPermissionsToMemberCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<AddPermissionsToRoleCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<AddRolesToMemberCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<CreateClubCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<CreateRoleCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<DeleteClubCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<DeleteRoleCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<RemoveCocktailsCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<RemoveMemberCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<RemovePermissionsToMemberCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<RemovePermissionsToRoleCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<RemoveRolesToMemberCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UpdateAddressCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UpdateDescriptionCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UpdateNameCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UpdateOwnerCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UpdateRoleNameCommandHandler>();
                cfg.RegisterServicesFromAssemblyContaining<UpdateVisibilityCommandHandler>();
                // -- Query Handler
                cfg.RegisterServicesFromAssemblyContaining<GetClubByIdQueryHandler>();
                
                // -- Event Hanlders
                cfg.RegisterServicesFromAssemblyContaining<ClubDeletedEventHandler>();
            });
    
            return services;
        }

        public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
        {
            // Auth
            services.AddValidatorsFromAssemblyContaining<SignInCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<SignUpCommandValidator>();

            // Club
            services.AddValidatorsFromAssemblyContaining<AddCocktailsCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<AddMemberCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<AddPermissionsToMemberCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<AddPermissionsToRoleCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<AddRolesToMemberCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateClubCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateRoleCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteClubCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteRoleCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<RemoveCocktailsCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<RemoveMembersCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<RemovePermissionsToMemberCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<RemovePermissionsToRoleCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<RemoveRolesToMemberCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAddressCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateDescriptionCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateNameCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateOwnerCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateRoleNameCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateVisibilityCommandValidator>();

            // Pipeline behaviour
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddCustomErrors(this IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => false; // 🔒 Never include stack traces

                options.Map<ValidationException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = ex.Message
                    });

                options.Map<ArgumentException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = ex.Message
                    });

                options.Map<KeyNotFoundException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = ex.Message
                    });

                options.Map<UnauthorizedAccessException>(ex =>
                    new ProblemDetails
                    {
                        Title = "Forbidden",
                        Status = StatusCodes.Status403Forbidden,
                        Detail = ex.Message
                    });
            });
            return services;
        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-US");

                var cultures = new CultureInfo[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr-FR"),
                };

                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SchemaFilter<ClubPermissionSchemaFilter>();

                // Define the BearerAuth scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                // Apply the BearerAuth scheme globally to all operations
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}
