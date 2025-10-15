using SimpleTaskApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

// Для тестирования в CI/CD - завершать после запуска
if (Environment.GetEnvironmentVariable("CI") == "true")
{
    // В CI окружении запускаем и завершаем
    var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Application started in CI mode, will shutdown in 10 seconds");
    
    // Завершить через 10 секунд
    _ = Task.Delay(10000).ContinueWith(_ => 
    {
        logger.LogInformation("Shutting down in CI environment");
        lifetime.StopApplication();
    });
}

app.Run();