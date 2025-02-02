using DotNetEnv;
using NetworkData.Repositories;
using NetworkData.Services;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

string connectionString = Env.GetString("DB_CONNECTION_STRING");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services and repositories with connection string injection
builder.Services.AddScoped<INetworkStatsRepository>(provider => new NetworkStatsRepository(connectionString));
builder.Services.AddScoped<INetworkStatsService, NetworkStatsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine($"Database connection established successfully with connection string: {connectionString}");

app.Run();
