using Microsoft.EntityFrameworkCore;
using Personnel.Api.Middleware;
using Personnel.Application.Services.Interfaces;
using Personnel.Application.Services.Services;
using Personnel.Infrastructure.Data;
using Personnel.Infrastructure.Minio;
using Personnel.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StaffHomeProDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IPersonService, PersonServices>();

builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddSingleton<MinioStorageService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StaffHomeProDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});
app.MapGet("/api/ping", () => "pong");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();