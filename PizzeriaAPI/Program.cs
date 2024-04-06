using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Pizzeria.Domain;
using Pizzeria.Domain.Extensions;
using Pizzeria.Domain.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PizzeriaDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DatabaseSQL"),
    b => b.MigrationsAssembly("Pizzeria.Domain")));

// Add repositories
builder.Services.RegisterRepositories();

// Add services
builder.Services.RegisterServices();

// Database Seeder
builder.Services.AddScoped<ISeeder, Seeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
