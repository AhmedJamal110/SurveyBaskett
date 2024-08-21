using SurveyBasket.API.Services;

namespace SurveyBasket.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesConfigration(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContect>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddScoped<IPollService, PollService>();

            // FluentValidation
            services.AddFluenentValidtionConfigration();
            
            // Mapster
            services.AddMapsterConfigration();
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


    }
}
