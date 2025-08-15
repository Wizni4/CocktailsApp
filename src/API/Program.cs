using CocktailsApp.API.Configuration;
using CocktailsApp.Application;
using CocktailsApp.Infrastructure;
using CocktailsApp.ReadStore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddReadStore(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddWebApi(builder.Configuration);

var app = builder.Build();
app.UseWebApi();
app.Run();
