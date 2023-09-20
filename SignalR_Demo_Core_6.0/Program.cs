namespace SignalR_Demo_Core_6._0
{
    /// <summary>
    /// 项目模板使用的是Asp.NetCore.MinimalApi
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            //添加SignalR服务的依赖注入
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();



            //指定Hub服务的路由。该方法要在app.MapControllers()方法之前执行。
            app.MapHub<SignalR_Hub>("/ChatHub");

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                return GetWeatherForecasts();
            });
            app.Run();
        }

        public static List<WeatherForecast> GetWeatherForecasts()
        {
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecastList = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    }).ToList();
            return forecastList;
        }
    }
}