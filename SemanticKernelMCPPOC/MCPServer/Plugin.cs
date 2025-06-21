using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MCPClient.Plugin
{
    public class DateTimeUtils
    {
        [KernelFunction, Description("Retrieves the current date time in UTC")]
        public static string GetCurrentDateTimeInUtc()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    public class WeatherUtils
    {
        [KernelFunction, Description("Gets the current weather for the specified city and specified date time.")]
        public static string GetCurrentWeather(string cityName)
        {
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
