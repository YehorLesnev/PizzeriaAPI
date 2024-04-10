using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Pizzeria.Domain;
using Pizzeria.Domain.Extensions;
using Pizzeria.Domain.Seeder;
using PizzeriaAPI.Identity.Extensions;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Authorization
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PizzeriaDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Auth",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

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

app.MapIdentityApi<IdentityUser>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Add roles
await app.AddIdentityRoles();

app.Run();