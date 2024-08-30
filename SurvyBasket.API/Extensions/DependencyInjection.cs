using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SurveyBasket.ApI.JwtService;
using System.Text;

namespace SurveyBasket.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesConfigration(this IServiceCollection services , IConfiguration configuration)
        {

            //CorsOrigin
            var allowOrigin = configuration.GetSection("AllowedOrigins").Get<string[]>();
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(opt =>
                {
                    opt
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(allowOrigin!);


                });
            });

            // Swagger Documintation
            services.AddSwaggerDocumentationServices();

            services.AddDbContext<ApplicationDbContect>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // FluentValidation
            services.AddFluenentValidtionConfigration();
            
            // Mapster
            services.AddMapsterConfigration();

            // Identity
            services.AddIdentityServiceConfigration(configuration);
            return services;
        }

        private static IServiceCollection AddSwaggerDocumentationServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(C =>
            {
                var SecuritySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorizations",
                    Description = " Jwt Auth Bearer Schema",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,

                    }
                };

                C.AddSecurityDefinition("Bearer", SecuritySchema);
                var ScurityRequirments = new OpenApiSecurityRequirement
                {
                    {
                        SecuritySchema , new [] {"Bearer"}
                    }
                };

                C.AddSecurityRequirement(ScurityRequirments);
            });
            return services;



        }

        private static IServiceCollection AddFluenentValidtionConfigration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    return services;
        }

        private static IServiceCollection AddMapsterConfigration(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(config));
            return services;
        }

        private static IServiceCollection AddIdentityServiceConfigration(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                       .AddEntityFrameworkStores<ApplicationDbContect>()
                       .AddDefaultTokenProviders();

            services.AddOptions<JwtOption>()
                        .BindConfiguration(JwtOption.SectionName)
                        .ValidateDataAnnotations()
                        .ValidateOnStart();

            var settingsJwt = configuration.GetSection(JwtOption.SectionName).Get<JwtOption>();

            services.AddAuthentication( option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settingsJwt?.Key!)),
                        ValidateIssuer = true,
                        ValidIssuer = settingsJwt?.ValidIssuer,
                        ValidateAudience = true,
                        ValidAudience = settingsJwt?.ValidAudiance,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });


            return services;
        }
    }
}
