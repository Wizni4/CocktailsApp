using API.Configurations;

using CocktailsApp.API;
using CocktailsApp.API.Mappers;
using CocktailsApp.Application.SeedWork.Mappers;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200") // Replace with your front-end URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddMediatR(builder.Configuration);
builder.Services.AddAutoMapper(typeof(APIMapperProfile),typeof(ApplicationMapperProfile));
builder.Services.AddRepositories();
builder.Services.AddEventDispatcher();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationValidators();
builder.Services.AddCustomErrors();

builder.Services.ConfigureOptions<JwtBearerConfigureOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// Ensure UseCors is called before UseAuthorization
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseProblemDetails();

app.MapControllers();

app.Run();
