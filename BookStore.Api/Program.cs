using BookStore.Data.DbContexts;
using BookStore.Service.Interfaces;
using BookStore.Service.Mappers;
using BookStore.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddDbContext<BookStoreDbContext>();
builder.Services.AddAutoMapper(expression => expression.AddProfile<MapperProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();