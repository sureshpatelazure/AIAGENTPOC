using AIAgentPOC;
using AIAgentPOC.PizzaOrderAIAgentDemo.Plugin;
using AIAgentPOC.AgentOrchestration;
internal class Program
{
    private static async Task Main(string[] args)
    {
        List<Object> Plugins = new List<object>();

        // Pizza  Order
        //Plugins.Add(new PizzaPlugin());
        //await DemoApplication.Run("PizzaOrder", Plugins);

        //Plugins.Add(new TripPlanner());
        //Plugins.Add(new TimeTeller());
        //Plugins.Add(new ElectricCar());
        //Plugins.Add(new WeatherForecaster());

        //await DemoApplication.Run("TripPlanner", Plugins);

        //Physic expert
        // await DemoApplication.Run("PhysicsExpert");

        // RAG Demo
        await DemoApplication.Run("FriendlyAssistant");
        
        // DemoAgentOrchestration.RunOrchestration(AIAgentLib.Model.AgentOrchestrationPattern.Concurrent, "What is temperature?");
    }

}   