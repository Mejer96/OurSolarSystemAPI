using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OurSolarSystemAPI.Repository.MySQL;
using OurSolarSystemAPI.Repository.NEO4J;
using OurSolarSystemAPI.Service.MySQL;
using OurSolarSystemAPI.Service.NEO4J;
using OurSolarSystemAPI.Service.MongoDB;
// using OurSolarSystemAPI.Service;
using Microsoft.OpenApi.Models;
using Neo4j.Driver;
using OurSolarSystemAPI.Repository;
using OurSolarSystemAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models; // For Swagger and OpenAPI configurations
using Microsoft.AspNetCore.Authorization; // For [Authorize] attribute
using Microsoft.AspNetCore.Mvc;
using OurSolarSystemAPI.Repository.MongoDB; // For API controllers and IActionResult

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://test-issuer.com",
            ValidAudience = "test-audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c174ae8c1db30cfd12aa6b013ba99f71445267994b06cd18752c12a874a72afa"))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Allow requests from localhost:3000
               .AllowAnyHeader() // Allow all headers
               .AllowAnyMethod(); // Allow all HTTP methods
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("admin");
    });
});

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddScoped<ScrapingService>();

// MySQL
builder.Services.AddDbContext<OurSolarSystemContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 2)) // Adjust based on your MySQL version
    )
);
builder.Services.AddScoped<ScrapingServiceMySQL>();
builder.Services.AddScoped<PlanetServiceMySQL>();
builder.Services.AddScoped<BarycenterRepositoryMySQL>();
builder.Services.AddScoped<PlanetRepositoryMySQL>();
builder.Services.AddScoped<MoonRepositoryMySQL>();
builder.Services.AddScoped<ArtificialSatelliteRepositoryMySQL>();
builder.Services.AddScoped<UserRepositoryMySQL>();

// NEO4J
builder.Services.AddSingleton(GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "Password123")));
builder.Services.AddScoped<BarycenterRepositoryNEO4J>();
builder.Services.AddScoped<MoonRepositoryNEO4J>();
builder.Services.AddScoped<PlanetRepositoryNEO4J>();
builder.Services.AddScoped<MigrationServiceNEO4J>();
builder.Services.AddScoped<ArtificialSatelliteRepositoryNEO4J>();
builder.Services.AddScoped<EphemerisRepositoryNEO4J>();


// MongoDB
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<BarycenterRepositoryMongoDB>();
builder.Services.AddScoped<PlanetRepositoryMongoDB>();
builder.Services.AddScoped<ArtificialSatelliteRepositoryMongoDB>();
builder.Services.AddScoped<EphemerisRepositoryMongoDB>();
builder.Services.AddScoped<MigrationServiceMongoDB>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Set up the Bearer token authentication in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Description = "Enter JWT Bearer token (e.g., 'Bearer {token}')",
    });

    // Apply the Bearer token security to the Swagger UI
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OurSolarSystemContext>();
        dbContext.Database.EnsureCreated(); // This creates the database if it does not exist
    }


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost3000");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
