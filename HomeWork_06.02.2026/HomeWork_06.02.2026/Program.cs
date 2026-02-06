using HomeWork_06._02._2026.Repositories.AuthorRepository;
using HomeWork_06._02._2026.Repositories.BookRepository;
using HomeWork_06._02._2026.Services.AuthorService;
using HomeWork_06._02._2026.Services.BookService;
using HomeWork_06._02._2026.Services.ValidDescriptionService;
using HomeWork_06._02._2026.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("bookStore"));
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddSingleton<IValidDescriptionService, ValidDescriptionService>();

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