using ignite.Application.Interfaces;
using ignite.Application.Services.Commands;
using ignite.Application.Services.Handlers;
using ignite.Application.Services.Queries;
using ignite.Domain.Models;
using ignite.Infrastructure.Data;
using ignite.Infrastructure.DependencyInjection;
using ignite.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração dos serviços
builder.Services.AddControllers();

// Registro dos serviços
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<UserHandlerService>();
builder.Services.AddScoped<UserQueryService>();
builder.Services.AddScoped<UserCommandService>();
builder.Services.AddScoped<GoalQueryService>();
builder.Services.AddScoped<GoalCommandService>();
builder.Services.AddScoped<GoalHandlerService>();
builder.Services.AddScoped<CategoryQueryService>();
builder.Services.AddScoped<CategoryCommandService>();
builder.Services.AddScoped<CategoryHandlerService>();
builder.Services.AddScoped<AuthCommandService>();
builder.Services.AddScoped<AuthHandlerService>();

// Configuração do NSwag (Swagger)
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Sua API";
    document.Version = "v1";

    document.AddSecurity("JWT", new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira o token JWT no formato: Bearer {seu_token}"
    });

    document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

// Configuração da autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    // Configuração do NSwag
    app.UseOpenApi();
    app.UseSwaggerUi(settings =>
    {
        settings.Path = "/swagger";
        settings.DocumentPath = "/swagger/v1/swagger.json";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();