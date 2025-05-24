namespace AIAgentPOC.PizzaOrderAIAgentDemo.Core.Model
{
    public class Pizza
    {
        public long Id { get; set; }
        public string? Name { get; set;}
        public string? Description { get; set;}
        public decimal Price { get; set;}
    }

    public class PizzaTopping
    {
        public long Id { get; set; }
        public string? Name { get; set;}
    }

    public class PizzaSize
    {
        public long Id { get; set; }
        public string? Size { get; set; }
    }

    public class PizzaCart
    {
        public long Id { get; set;}
        public Pizza? Pizza { get; set;}
        public PizzaSize? Size { get; set; }
        public List<PizzaTopping>? Toppings { get; set; }
        public int Quantity { get; set; }

        public string? SpecialInstruction { get; set; }
       
    } 
}
