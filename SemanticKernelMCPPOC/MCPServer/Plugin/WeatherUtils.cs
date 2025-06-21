using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MCPServer.Plugin
{
    public class WeatherUtils
    {
        [KernelFunction, Description("Gets the current weather for the specified city and specified date time.")]
        public static string GetCurrentWeather(string cityName)
        {
            Console.WriteLine($"Getting current weather for {cityName}...");    
            return cityName switch
            {
                "Boston" => "61 and rainy",
                "London" => "55 and cloudy",
                "Mumbai" => "85 and sunny",
                "Parkstead" => "70 and humid",
                "Melland" => "75 and sunny",
                "North Eastham" => "80 and sunny",
                _ => "100 and sunny",
            };
        }
    }
}