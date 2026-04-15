using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("-----------------------");
            Console.WriteLine(" Shopping Cart System");
            Console.WriteLine("-----------------------");


            //Store Display Menu
            Product[] products = new Product[5];

            products[0] = new Product { ID = 1, Name = "Apple", price = 10, RemainingStock = 15 };
            products[1] = new Product { ID = 2, Name = "Banana", price = 12, RemainingStock = 10 };
            products[2] = new Product { ID = 3, Name = "Orange", price = 13.25, RemainingStock = 12 };
            products[3] = new Product { ID = 4, Name = "Onions", price = 7, RemainingStock = 15 };
            products[4] = new Product { ID = 5, Name = "Golden Shovel", price = 5000, RemainingStock = 3 };

            foreach (Product i in products)
            {
                i.DisplayProduct();
            }

            //Cart
            Product[] cart = new Product[3];
            int[] cart_quantity = new int[3];
            int cart_count = 0;

            String answer;

            do
            {

                //User input
                int choice;
                int quantity;

                Console.Write("Enter product number: ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }


                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity");
                    continue;
                }

                //Checks if Product Number is in the array
                if (choice < 1 || choice > products.Length)
                {
                    Console.WriteLine("Invalid product number!");
                    continue;
                }

                //Checks if there is available stock left
                Product selected = products[choice - 1];

                if (selected.RemainingStock == 0)
                {
                    Console.WriteLine("Out of Stock");
                    continue;
                }

                if (!selected.HasEnoughStock(quantity))
                {
                    Console.WriteLine("Not enough stock available.");
                    continue;
                }


                //Prevent duplicated products in the cart
                bool found = false;

                for (int i = 0; i < cart_count; i++)
                {
                    if (cart[i] != null && cart[i].ID == selected.ID)
                    {
                        cart_quantity[i] += quantity;
                        found = true;
                        break;
                    }
                }

                //Add products to the cart
                if (!found)
                {
                    if (cart_count >= cart.Length)
                    {
                        Console.WriteLine("Cart is Full");
                        continue;
                    }

                    cart[cart_count] = selected;
                    cart_quantity[cart_count] = quantity;
                    cart_count++;

                }

                selected.DeductStock(quantity);
                Console.WriteLine("Added to cart");

                //Asking the users if they want to continue
                Console.Write("Do you want to Continue using the system? (Y/N): ");
                answer = Console.ReadLine();

            } while (true);



            Console.ReadKey();
        }

    }
    class Product
    {
        public int ID;
        public String Name;
        public double price;
        public int RemainingStock;

        public void DisplayProduct()
        {
            Console.WriteLine($"|{ID}| {Name} - {price} php (Stock: {RemainingStock})");
        }

        public double GetItemTotal(int quantity)
        {
            return price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return quantity <= RemainingStock;
        }

        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }
    }
}
