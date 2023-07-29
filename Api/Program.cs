using Business;
using Business.Producer;
using Business.Providers;
using Integration.Elastic.DAL;
using Integration.Elastic.Providers;
using Nest;
using Types.Interfaces.BusinessInterfaces;
using Types.Interfaces.DataAccessInterfaces;
using Types.Interfaces.ProviderInterfaces;
using Types.Models;
using WorkerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDatabaseConfigurationProvider, DatabaseConfigurationProvider>();
builder.Services.AddSingleton<IDatabaseInitializationProvider, ElasticDatabaseInitializationProvider>();
builder.Services.AddSingleton<IProductDAL, ProductDAL>();
builder.Services.AddSingleton<IProductBLL, ProductBLL>();

//worker service implementation
builder.Services.AddHostedService<Worker>();


//rabbitMQProducer implemantation
builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();
builder.Services.AddSingleton<IMessageConsumer, RabbitMQConsumer>();

//redis implemantation

builder.Services.AddStackExchangeRedisCache(option => option.Configuration = "localhost:1453");

var app = builder.Build();

IDatabaseInitializationProvider databaseInitializationProvider = app.Services.GetRequiredService<IDatabaseInitializationProvider>();
databaseInitializationProvider.InitializeDatabase().Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:3000"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
