using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Pizzeria.Domain;
using Pizzeria.Domain.Extensions;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Seeder;
using PizzeriaAPI.Extensions;
using PizzeriaAPI.Identity.JwtConfig;
using PizzeriaAPI.Middleware;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddDbContext<PizzeriaDbContext>(options =>
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("DatabaseSQL"),
            b => b.MigrationsAssembly("Pizzeria.Domain"))
        .EnableSensitiveDataLogging());

builder.Services.Configure<JwtTokenConfig>(
    builder.Configuration.GetSection(nameof(JwtTokenConfig)));

builder.Services.AddSingleton<IJwtTokenConfig>(x =>
    x.GetRequiredService<IOptions<JwtTokenConfig>>().Value);

// Identity
builder.Services.AddIdentity<Customer, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<PizzeriaDbContext>()
    .AddDefaultTokenProviders();

// Authentication
builder.Services.AddJwtAuthentication(
    builder.Configuration.GetValue<string>($"{nameof(JwtTokenConfig)}:JwtIssuer"),
    builder.Configuration.GetValue<string>($"{nameof(JwtTokenConfig)}:JwtAudience"),
    builder.Configuration.GetValue<string>($"{nameof(JwtTokenConfig)}:JwtKey"));

// Authorization
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Add repositories
builder.Services.RegisterRepositories();

// Add services
builder.Services.RegisterServices();

// Database Seeder
builder.Services.AddScoped<ISeeder, Seeder>();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Development.DockerCompose"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.AddSerilog();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
    },

    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Static")),

    RequestPath = "/static"
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

// Add roles
await app.AddIdentityRoles();

app.Run();