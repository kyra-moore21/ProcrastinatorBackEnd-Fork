using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200",
                               "https://theprioritizer.netlify.app")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});

var app = builder.Build();

// Determine the port
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Comment out HTTPS redirection for Heroku
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

// Use CORS
app.UseCors();
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://+:{port}");
