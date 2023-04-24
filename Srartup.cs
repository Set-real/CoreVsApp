using Microsoft.AspNetCore.Builder;
using System.Runtime.Intrinsics.X86;

public class Startup
{
    // Создал глобальную статическую переменную только для  чтения, для работы в статических классах
    static readonly IWebHostEnvironment? env;

    // Метод вызывается средой ASP.NET.
    // Используйте его для подключения сервисов приложения
    // Документация:  https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
    }

    /// <summary>
    ///  Обработчик для страницы About
    /// </summary>
    private static void About(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync($"{env?.ApplicationName} - ASP.Net Core tutorial project");
        });
    }

    /// <summary>
    ///  Обработчик для главной страницы
    /// </summary>
    private static void Config(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            await context.Response.WriteAsync($"App name: {env?.ApplicationName}. App running configuration: {env.EnvironmentName}");
        });
    }

    // Метод вызывается средой ASP.NET.
    // Используйте его для настройки конвейера запросов
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Проверяем, не запущен ли проект в среде разработки
        if (env.IsDevelopment())
        {
            // 1. Добавляем компонент, отвечающий за диагностику ошибок
            app.UseDeveloperExceptionPage();
        }

        // 2. Добавляем компонент, отвечающий за маршрутизацию
        app.UseRouting();

        //Добавляем компонент для логирования запросов с использованием метода Use.
        app.Use(async (context, next) =>
        {
            // Для логирования данных о запросе используем свойства объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            await next.Invoke();
        });

        //Добавляем компонент с настройкой маршрутов для главной страницы
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
            });
        });
        
        // Все прочие страницы имеют отдельные обработчики
        app.Map("/about", About);
        app.Map("/config", Config);

        // Обработчик для ошибки "страница не найдена"
        app.Run(async (context) =>
        {
            await context.Response.WriteAsync($"Page not found");
        });
    }    
}