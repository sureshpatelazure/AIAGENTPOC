namespace AIAgentPOC.AIAgentPluginDemo.PizzaAIAgent
{
    public class PizzaAIAgent
    {
        //IConfiguration _configuration;
        //IAIConnectorService _aIConnectorService;
        //Kernel _Kernel;
        //ChatCompletionAgent _agent;

        //public PizzaAIAgent (IAIConnectorService aIConnectorService, IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _aIConnectorService = aIConnectorService;
        //    _Kernel = CreateKernel();
        //    _agent =  CreateAgent();
        //}

        //private Kernel CreateKernel()
        //{
        //    List<Object> Plugins =  new List<object>();
        //    Plugins.Add(new PizzaPlugin());

        //    return _aIConnectorService.BuildChatCompletionKernelWithPlugin(_configuration, Plugins);
        //}

        //private AIAgentInput GetAgentProfile()
        //{
        //    AIAgentInput aIAgentInput = new AIAgentInput();
        //    aIAgentInput.Instructions = """
        //        You are a friendly assistant who will take pizza order from user. 
        //        Ask user to select pizza, toppings and size beforing adding to cart.
        //        User can order mulitple pizza.
        //        Display selected cart before proceding order.
        //        Ask user for adding/removing pizza from cart before ordering.
        //        Require user approval for completing order
        //        If the user doesn't provide enough information for you to complete a order, you will keep asking questions until you have
        //        enough information to complete the order.
        //        Display sentence on new line.
        //        """;

        //    aIAgentInput.Name = "Pizza Order AI Agent ";
        //    aIAgentInput.Kernel = _Kernel;

        //    return aIAgentInput;
        //}

        //private ChatCompletionAgent CreateAgent()
        //{
        //    AIAgentInput agentInput = GetAgentProfile();
        //    ChatCompletionAgent agent = new()
        //    {
        //        Instructions = agentInput.Instructions,
        //        Name = agentInput.Name,
        //        Kernel = agentInput.Kernel,
        //        Arguments = new KernelArguments(
        //             new PromptExecutionSettings
        //             {
        //                 FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        //             }
        //        )
        //    };

        //    return agent;
        //}

        //public async Task StartPizzaOrder()
        //{
        //    ChatHistoryAgentThread chatHistoryAgentThread = new ChatHistoryAgentThread();

        //    await IntroduceAIAgent(chatHistoryAgentThread);

        //    bool isComplete = false;

        //    do
        //    {
        //        Console.WriteLine();
        //        Console.Write("User> ");
        //        string input = Console.ReadLine();

        //        if (string.IsNullOrWhiteSpace(input))
        //        {
        //            continue;
        //        }

        //        if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
        //        {
        //            isComplete = true;
        //            break;
        //        }

        //        var message = new ChatMessageContent(AuthorRole.User, input);

        //        Console.WriteLine();
        //        Console.WriteLine("Assistant> Please Wait.......");
        //        Console.WriteLine();

        //        await foreach (StreamingChatMessageContent response in _agent.InvokeStreamingAsync(message, chatHistoryAgentThread))
        //        {
        //            Console.Write(response.Content);
        //        }

        //        Console.WriteLine();

        //    } while (!isComplete);

        //    await chatHistoryAgentThread.DeleteAsync();
        //}
       
        //private async Task IntroduceAIAgent(ChatHistoryAgentThread chatHistoryAgentThread) {

        //    string input = "Who are you?";
        //    var message = new ChatMessageContent(AuthorRole.User, input);

        //    Console.WriteLine();
        //    Console.Write("Assistant>Please Wait.....");
            
        //    await foreach (StreamingChatMessageContent response in _agent.InvokeStreamingAsync(message, chatHistoryAgentThread))
        //    {
        //        Console.Write(response.Content);
        //    }

        //    Console.WriteLine();
        //}
    }
}
