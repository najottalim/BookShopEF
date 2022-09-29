using BookStore.Data.DbContexts;
using BookStore.Service.Helpers;
using BookStore.Service.Interfaces;
using BookStore.Service.Mappers;
using BookStore.Service.Middlewares;
using BookStore.Service.Services;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HerokuPostgres");
var botToken = builder.Configuration.GetSection("TelegramBotToken")["Production"];
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
builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(c =>
    new TelegramBotClient(botToken));
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();