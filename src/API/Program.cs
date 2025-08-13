
using CocktailsApp.API.SeedWork;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// -- Base services
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// -- Custom services
builder.Services.AddCustomCors();
builder.Services.AddCustomErrors();
builder.Services.AddCustomSwagger();

// -- Dependencies injections
builder.Services.AddRepositories();
builder.Services.AddReaders();
builder.Services.AddProjectors();
builder.Services.AddAutoMapper();
builder.Services.AddMediatR(builder.Configuration);
builder.Services.AddApplicationValidators();
builder.Services.AddApplicationServices();
builder.Services.AddBackgroundServices();

// -- Environment configuration
builder.ConfigureEnvironment();

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

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseProblemDetails();

app.MapControllers();

app.Run();
