using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AIAgentPOC.TripPlannerAIAgentDemo.Plugin
{
    public class WeatherForecaster
    {
        [KernelFunction]
        [Description("This function retrieves weather at given destination.")]
        [return: Description("Weather at given destination.")]
        public string GetTodaysWeather([Description("The destination to retrieve the weather for.")] string destination)
        {
            string[] weatherPatterns = { "Sunny", "Cloudy", "Windy", "Rainy", "Snowy" };
            Random rand = new Random();
            return weatherPatterns[rand.Next(weatherPatterns.Length)];
        }
    }
}
