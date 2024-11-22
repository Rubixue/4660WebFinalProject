using _4660FinalProject.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS to allow Blazor WebAssembly requests
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7230", "http://localhost:5144") // Blazor client URLs
              .AllowAnyHeader() // Allow any headers, e.g., Content-Type, Authorization
              .AllowAnyMethod(); // Allow HTTP methods like GET, POST, etc.
    });
});

// Register application services
builder.Services.AddScoped<TemporalCoalescingService>();
builder.Services.AddScoped<JSONToMySQLService>();

// Add Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

app.UseCors(); // Apply CORS policy

app.UseAuthorization(); // Ensure Authorization middleware is active

app.MapControllers(); // Map API controllers to endpoints

app.Run();
