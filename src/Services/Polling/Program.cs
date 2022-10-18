using Microsoft.EntityFrameworkCore;
using Polling.Dal;
using Polling.Repository.Implementations;
using Polling.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration["Configuration:DefaultConnection"];

builder.Services.AddDbContext<PollingContext>(options =>
{
    options.UseNpgsql(connection);
});

builder.Services.AddScoped<IPollService, PollService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
