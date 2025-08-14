using CocktailsApp.API.Configuration;
using CocktailsApp.Application;
using CocktailsApp.Infrastructure;
using CocktailsApp.ReadStore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddReadStore(builder.Configuration);

var app = builder.Build();
app.UseWebApi(app.Environment);
app.Run();
