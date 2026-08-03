using Microsoft.EntityFrameworkCore;
using Registration.Application.Interfaces;
using Registration.Application.Services;
using Registration.Infrastructure.Data;
using Registration.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Registration.Infrastructure")));

// Register Repositories
builder.Services.AddScoped<IMasterDataRepository, MasterDataRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IPortalLinkRepository, PortalLinkRepository>();

// Register Application Services
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<IPortalLinkService, PortalLinkService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Redirect root to swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    if (context.Request.Headers.TryGetValue("Access-Control-Request-Private-Network", out var _))
    {
        context.Response.Headers.Add("Access-Control-Allow-Private-Network", "true");
    }
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
