var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy("LocalhostCorsPolicy", policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(origin => true)
    )
);

// Add DbContext

// Repositories

// Services

// Controllers

builder.Services.AddControllers();


var app = builder.Build();

app.UseCors("LocalhostCorsPolicy");

app.UseAuthorization();

// Use Middleware

app.MapControllers();

app.Run();
