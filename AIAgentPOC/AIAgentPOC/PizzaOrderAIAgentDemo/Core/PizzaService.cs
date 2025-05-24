using AIAgentPOC.PizzaOrderAIAgentDemo.Core.Model;

namespace AIAgentPOC.PizzaOrderAIAgentDemo.Core.Service
{
    public class PizzaService
    {
        private List<PizzaCart> _cartList = new List<PizzaCart>();
        private int _cartId = 0;

        public List<Pizza> GetPizzaMenu()
        {
            List<Pizza> pizzas = new List<Pizza>();
            pizzas.Add(new Pizza { Id = 1, Name = "Margherita Pizza", Price = 100 });
            pizzas.Add(new Pizza { Id = 2, Name = "Paneer Pizza", Price = 200 });
            pizzas.Add(new Pizza { Id = 3, Name = "Farmouse Pizza", Price = 300 });
            pizzas.Add(new Pizza { Id = 4, Name = "White Pizza", Price = 400 });
            pizzas.Add(new Pizza { Id = 5, Name = "Chicken BBQ Pizza", Price = 500 });

            return pizzas;
        }

        public List<PizzaTopping> GetPizzaToppingMenu()
        {
            List<PizzaTopping> pizzaToppings = new List<PizzaTopping>();
            pizzaToppings.Add(new PizzaTopping { Id = 1, Name = "Extra Cheeze" });
            pizzaToppings.Add(new PizzaTopping { Id = 2, Name = "Onion" });
            pizzaToppings.Add(new PizzaTopping { Id = 3, Name = "Paneer" });
            pizzaToppings.Add(new PizzaTopping { Id = 4, Name = "Olive" });
            pizzaToppings.Add(new PizzaTopping { Id = 5, Name = "tommato" });
            return pizzaToppings;
        }

        public List<PizzaSize> GetPizzaSizeMenu() {

            List<PizzaSize> pizzaSizes = new List<PizzaSize>();
            pizzaSizes.Add(new PizzaSize { Id = 1, Size = "Small" });
            pizzaSizes.Add(new PizzaSize { Id = 2, Size = "Medium" });
            pizzaSizes.Add(new PizzaSize { Id = 3, Size = "Large" });

            return pizzaSizes;
        }

        public List<PizzaCart> AddPizzaToCart(
            Pizza pizza,
            PizzaSize pizzaSize, 
            List<PizzaTopping> pizzaToppings,
            int quantity, 
            string specialInstructions)
        {
            PizzaCart pizzaCart = new PizzaCart();
            pizzaCart.Id = ++_cartId;
            pizzaCart.Pizza= pizza;
            pizzaCart.Quantity = quantity;
            pizzaCart.Size = pizzaSize;
            pizzaCart.Toppings = pizzaToppings;
            pizzaCart.SpecialInstruction = specialInstructions;

            _cartList.Add(pizzaCart);

            return _cartList;

        }

        public void RemovePizzaFromCart(int CartId)
        {
            if (_cartList != null)
            {
                PizzaCart pizaCart = _cartList.FirstOrDefault(p => p.Id == _cartId);
                if (pizaCart != null)
                {
                    _cartList.Remove(pizaCart);
                }
            }
        }

        public List<PizzaCart> GetCart()
        {
            return _cartList;

        }

        public void Checkout(int cartId)
        {
            RemovePizzaFromCart(cartId);
        }
    }
}
