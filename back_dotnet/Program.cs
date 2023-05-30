using Microsoft.EntityFrameworkCore;
using Domain;
using Data;
using Services;
using Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy("LocalhostCorsPolicy", policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(origin => true)
    )
);

// Add DbContext

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services

builder.Services.AddSingleton<IGameDataService, GameDataService>();
builder.Services.AddScoped<IUserService, UserService>();

// Controllers

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("LocalhostCorsPolicy");

app.UseAuthorization();

// Use Middleware

app.UseMiddleware<JwtMiddleware>();

app.UseRouting();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
}

app.Run();