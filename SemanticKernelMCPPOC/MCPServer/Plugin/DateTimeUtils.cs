using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MCPServer.Plugin
{
    public class DateTimeUtils
    {
        [KernelFunction, Description("Retrieves the current date time in UTC")]
        public static string GetCurrentDateTimeInUtc()
        {
            Console.WriteLine("Retrieving current date time in UTC...");
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}