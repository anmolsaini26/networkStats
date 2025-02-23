using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetworkData.Repositories;
using NetworkData.Services;
using Npgsql;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Load .env file
DotNetEnv.Env.Load();

// Database Connection String
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// Register Services
builder.Services.AddScoped<INetworkStatsService, NetworkStatsService>();
builder.Services.AddScoped<INetworkStatsRepository>(provider => new NetworkStatsRepository(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200") // Replace with frontend URL
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});


var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAngularApp");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
