using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Pizzeria.Domain;
using Pizzeria.Domain.Extensions;
using Pizzeria.Domain.Seeder;
using PizzeriaAPI.Extensions;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

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

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Development.DockerCompose"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.MapIdentityApi<IdentityUser>();

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