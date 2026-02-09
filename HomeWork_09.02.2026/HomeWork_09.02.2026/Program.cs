using HomeWork_09._02._2026.Interfaces;
using HomeWork_09._02._2026.Services;
using HomeWork_09._02._2026.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersSubDB;Trusted_Connection=True;"));
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();