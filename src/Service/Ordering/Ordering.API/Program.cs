using Ordering.API.Extension;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MigrateDatabase<OrderContext>((context, service) =>
{
    var logger = service.GetService<ILogger<OrderContextSeeds>>();
    OrderContextSeeds
        .SeedAsync(context, logger)
        .Wait();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
