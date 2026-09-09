using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Persistence;
using System.Runtime.CompilerServices;
using System.Text;

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
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

            services.AddDatabaseConnectionString(configuration);


            services.AddAuthConfig();
            return services;
        }

       private static IServiceCollection AddDatabaseConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            //Resiteration of Connection String
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection String 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            );

            return services;
        }
        private static IServiceCollection AddAuthConfig(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<IJwtProvider, JwtProvider>();

           services.AddIdentity<ApplicationUser, IdentityRole>()
                     .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options => {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(e => {
                    e.SaveToken = true;
                    e.TokenValidationParameters = new TokenValidationParameters { 
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                         ValidateAudience= true,
                         ValidateLifetime = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TYWT5taxv+JUqPfjDGyfMwCNNAQy0G50q5rGP0GwK/8=")),
                         ValidIssuer = "SurveyBasketApp",
                         ValidAudience = "SurveyBasketApp Users",

                    };
                });

            return services;
        }
    }
}
