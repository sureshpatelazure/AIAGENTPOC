using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIAgentPOC.AIAgentPluginDemo.Core.Service;
using AIAgentPOC.AIAgentPluginDemo.Core.Model;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AIAgentPOC.AIAgentPluginDemo.Plugin
{
    public class PizzaPlugin
    {
        PizzaService _pizzaService = new PizzaService();

        [KernelFunction("get_pizza_menu")]
        [Description("This Function will return List of Pizza Menus to users.")]
        public List<Pizza> GetPizzMenu()
        {
            return _pizzaService.GetPizzaMenu();
        }

        [KernelFunction("add_pizza_to_cart")]
        [Description("Add a pizza to the user's cart; returns updated the cart")]
        public List<PizzaCart> AddPizzaToCart(
            Pizza pizza,
            PizzaSize pizzasize,
            List<PizzaTopping> pizzatoppings,
            int pizzaquantity = 1,
            string specialInstruction = ""
            ) {

            return _pizzaService.AddPizzaToCart(pizza, pizzasize, pizzatoppings, pizzaquantity, specialInstruction);
        }

        [KernelFunction("remove_pizza_from_cart")]
        [Description("Remove a pizza from the user's cart; returns updated the cart")]
        public string RemovePizzaFromCart(int cartId)
        {
            _pizzaService.RemovePizzaFromCart(cartId);
            return "Pizza removed with cartid:" + cartId;
        }

        [KernelFunction("get_cart")]
        [Description("Returns the user's current cart")]
        public List<PizzaCart> GetCart()
        {
            return _pizzaService.GetCart(); 
        }

        [KernelFunction("checkout")]
        [Description("Checkouts the user's cart; this function will retrieve the payment from the user and complete the order.")]
        public string Checkout(int cartId)
        {
            _pizzaService.Checkout(cartId);

            return "Pizza Order Completed with CartId " + cartId + ". Thank you for ordering pizza";
        }
    }
}
