using Mapster;
using MapsterMapper;
using SurveyBasket.Api.Contracts.Validations;
using SurveyBasket.Api.Middlewares;
using SurveyBasket.Api.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register the Dependency Injection
builder.Services.AddScoped<IPollService, PollService>();

//builder.Services.AddScoped<IValidater<CreatePollRequest>, CreatePollRequestValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var mappingConfig = TypeAdapterConfig.GlobalSettings;
  mappingConfig.Scan(Assembly.GetExecutingAssembly());
//Add Mapster
builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

//var logger = app.Logger;
//
//app.Use(async (context, next) =>
//{
//    logger.LogInformation("Processing request");
//    await next(context);
//    logger.LogInformation("Processing Response");
//
//});

app.UseCustomMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
