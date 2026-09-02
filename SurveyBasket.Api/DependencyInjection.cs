

using Microsoft.EntityFrameworkCore;
using SurveyBasket.Api.Persistence;
using System.Runtime.CompilerServices;

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseConnectionString(configuration);

            // Add services to the container.

           services.AddControllers();//.AddFluentValidation();
                                    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                                    //builder.Services.AddOpenApi();
           
           
           services.AddEndpointsApiExplorer();
           services.AddSwaggerGen();


            //Register the Dependency Injection
            services.AddScoped<IPollService, PollService>();

          // services.AddScoped<IValidater<CreatePollRequest>, CreatePollRequestValidator>();
          // services.AddValidatorsFromAssemblyContaining<Program>();

           services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

           var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            //Add Mapster
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));


            return services;
        }

       public static IServiceCollection AddDatabaseConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            //Resiteration of Connection String
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection String 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}
