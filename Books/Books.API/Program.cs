using Books.API.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// register the DbContext on the container 
builder.Services.AddDbContext<BooksContext>(options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:BooksDBConnectionString"]));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
