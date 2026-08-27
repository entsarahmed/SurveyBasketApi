using SurveyBasket.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register the Dependency Injection
//builder.Services.AddScoped<IOS, WindowsOsService>();

builder.Services.AddTransient<IOperationTransient, WindowsOsService>();
builder.Services.AddScoped<IOperationScoped, WindowsOsService>();
builder.Services.AddSingleton<IOperationSingleton, WindowsOsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
