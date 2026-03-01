using CryptoProj.API;
using CryptoProj.API.Endpoints;
using CryptoProj.API.HostedServices;
using CryptoProj.API.Middlewares;
using CryptoProj.API.Records;
using CryptoProj.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

//builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddHostedService<CatsHostedService>();

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = "ExternalCookie";
})
.AddCookie("ExternalCookie")
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]!))
    };
})
.AddGoogle("Google", options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    options.CallbackPath = "/api/v1/users/google-callback";
});

// Redis for Distributed Caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "SampleApp:";
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

//app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthentication(); // хто ти
app.UseAuthorization();  // що ти можеш

app.MapControllers();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke(context);
    }
    catch (Exception ex)
    {
        var statusCode = ex switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidCredentialsException => StatusCodes.Status400BadRequest,
            EmailAlreadyTakenException => StatusCodes.Status400BadRequest,
            NotFoundWithIdException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.Body.FlushAsync();
        var error = new Error(statusCode, ex.Message, ex.StackTrace);
        var errorJson = JsonSerializer.Serialize(error);
        await context.Response.WriteAsync(errorJson);
    }
});

app.MapCatsEndpoints();

app.Run();