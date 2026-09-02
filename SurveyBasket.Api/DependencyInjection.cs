

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
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
    }
}
