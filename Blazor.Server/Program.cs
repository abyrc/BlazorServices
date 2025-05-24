using Blazor.Server;
using Blazor.Server.Config;
using Blazor.Server.UseCases.Interfaces;
using Blazor.Server.UseCases.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure());
}

);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IGeminiUseCase, GeminiUseCase>();

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("nuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Aquí es donde pones el código para migrar
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();  // Aplica migraciones pendientes al iniciar
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor V1");
        c.RoutePrefix = string.Empty;
    });

}
app.UseCors("nuevaPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();
