using BookStore.Data.DbContexts;
using BookStore.Service.Interfaces;
using BookStore.Service.Mappers;
using BookStore.Service.Middlewares;
using BookStore.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HerokuPostgres");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseNpgsql(connectionString, optionsBuilder =>
        optionsBuilder.MigrationsAssembly("BookStore.Api")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddAutoMapper(expression => expression.AddProfile<MapperProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();