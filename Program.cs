using ignite.Infrastructure.Data;
using ignite.Infrastructure.Repositories;
using ignite.Services.Implementations;
using ignite.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILevelRepository, LevelRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILevelService, LevelService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
        {
            options.DocumentPath = "/openapi/v1.json";
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
