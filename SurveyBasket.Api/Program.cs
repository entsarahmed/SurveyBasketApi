
using SurveyBasket.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 

    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCustomMiddleware();

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
