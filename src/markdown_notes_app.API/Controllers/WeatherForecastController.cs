using markdown_notes_app.Core.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace markdown_notes_app.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILoggerManager loggerManager;

        public WeatherForecastController(ILoggerManager logger)
        {
            loggerManager = logger;
        }

        [HttpGet("{id}")]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            WeatherForecast ab = new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            loggerManager.LogInfo(JsonSerializer.Serialize(ab).ToString());

            return Enumerable.Range(1, 5).Select(id => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
