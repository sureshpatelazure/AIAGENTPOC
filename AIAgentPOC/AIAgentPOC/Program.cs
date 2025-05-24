using AIAgentPOC;
using AIAgentPOC.PizzaOrderAIAgentDemo.Plugin;

internal class Program
{
    private static async Task Main(string[] args)
    {

        //PizzaOrder pizzaorder = new PizzaOrder();
        //await pizzaorder.StartPizzaOrder();

        List<Object> Plugins = new List<object>();
        Plugins.Add(new PizzaPlugin());
        await  DemoApplication.Run("PizzaOrder", Plugins);

    }


}   