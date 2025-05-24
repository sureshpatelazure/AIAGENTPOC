using AIAgentPOC.PizzaOrderAIAgentDemo.PizzaOrder;

internal class Program
{
    private static async Task Main(string[] args)
    {

        PizzaOrder pizzaorder = new PizzaOrder();
        await pizzaorder.StartPizzaOrder();

    }


}   