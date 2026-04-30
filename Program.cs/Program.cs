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
            Console.WriteLine("|Shopping Cart System |");
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
            Console.WriteLine("------------------------");

            //Cart
            Product[] cart = new Product[3];
            int[] cart_quantity = new int[3];
            int cart_count = 0;

            String answer;
            bool outer = true;

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
                    Console.WriteLine("That product is out of stock");
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
                        break;
                    }

                    cart[cart_count] = selected;
                    cart_quantity[cart_count] = quantity;
                    cart_count++;

                }

                selected.DeductStock(quantity);
                Console.WriteLine($"{selected.Name} x{quantity} was added to cart");

                //Asking the users if they want to continue
                while (true)
                {
                    Console.Write("Do you want to add more product in the cart (Y/N)?: ");
                    answer = Console.ReadLine().ToUpper();

                    if (answer == "Y")
                    {
                        break;
                    }
                    else if (answer == "N")
                    {
                        outer = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        continue;
                    }
                }


            } while (outer);

            double grand_total = 0;
            double finaltotal = 0;
            double discount = 0;
            double payment;
            bool Checkout = true;

            while (Checkout)
            {
                int choice2;

                Console.WriteLine("\n-----------------------");
                Console.WriteLine("|      Cart Menu      |");
                Console.WriteLine("-----------------------");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Remove an item from cart");
                Console.WriteLine("3. Update item quantity");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Checkout");

                Console.Write("\nEnter Your choice: ");
                choice2 = Convert.ToInt32(Console.ReadLine());

                switch (choice2)
                {

                    case 1:

                        //check if you cart is empty
                        if (cart_count == 0)
                        {
                            Console.WriteLine("Your Cart is Empty!");
                            break;
                        }

                        //Shows your cart
                        Console.WriteLine("\nYour Cart: ");
                        for (int i = 0; i < cart_count; i++)
                        {
                            Console.WriteLine($"Item {i + 1}: {cart[i].Name} x{cart_quantity[i]}");
                        }
                        break;

                    case 2:

                        //check if you cart is empty
                        if (cart_count == 0)
                        {
                            Console.WriteLine("Your Cart is Empty!");
                            break;
                        }

                        //remove item in your cart
                        Console.Write("Which item to remove: ");

                        int removeItem = Convert.ToInt32(Console.ReadLine()) - 1;
                        if (removeItem < 0 || removeItem >= cart_count)
                        {
                            Console.WriteLine("Invalid Item!");
                            break;
                        }

                        for (int i = removeItem; i < cart_count; i++)
                        {
                            cart[i] = cart[i + 1];
                        }

                        cart_count--;

                        Console.WriteLine("Item succesfuly removed!");
                        break;

                    case 3:

                        //check if you cart is empty
                        if (cart_count == 0)
                        {
                            Console.WriteLine("Your Cart is Empty!");
                            break;
                        }

                        Console.Write("Select item to update: ");
                        int updateitem = Convert.ToInt32(Console.ReadLine()) - 1;

                        //checks if the item is in the cart
                        if (updateitem < 0 || updateitem >= cart_count)
                        {
                            Console.WriteLine("Invalid item");
                            break;
                        }

                        Console.Write("Enter New Quantity: ");
                        int updatequantity = Convert.ToInt32(Console.ReadLine());

                        if (!cart[updateitem].HasEnoughStock(updatequantity))
                        {
                            Console.WriteLine("Not enough stock!");
                            break;
                        }

                        if (updatequantity < 0)
                        {
                            Console.WriteLine("Quantity must be at leat 1!");
                                break;
                        }

                        cart_quantity[updateitem] = updatequantity;
                        Console.WriteLine("Quantity is updated successfully!");
                        break;

                    case 4:

                        //check if you cart is empty
                        if (cart_count == 0)
                        {
                            Console.WriteLine("Your Cart is Empty!");
                            break;
                        }

                        cart_count = 0;
                        Console.WriteLine("\nCart is Cleared");
                        break;

                    case 5:

                        //check if you cart is empty
                        if (cart_count == 0)
                        {
                            Console.WriteLine("Your Cart is Empty!");
                            break;
                        }

                        //Computes grand total
                        for (int i = 0; i < cart_count; i++)
                        {
                            double subtotal = cart[i].GetItemTotal(cart_quantity[i]);
                            grand_total += subtotal;
                        }


                        //Discount

                        if (grand_total >= 5000)
                        {
                            discount = grand_total * 0.10;
                        }

                        finaltotal = grand_total - discount;

                        Console.WriteLine($"\nFinal Total = php{finaltotal}");

                        while (true)
                        {
                            Console.Write("Enter payment: php");
                            if (!double.TryParse(Console.ReadLine(), out payment) || payment <= -1)
                            {
                                Console.WriteLine("Invalid Amount");
                                continue;
                            }

                            if (payment >= finaltotal)
                            {
                                Console.WriteLine($"Final Total: php{finaltotal}");
                                Console.WriteLine($"Payment: php{payment}");
                                Console.WriteLine($"Change: php{payment - finaltotal:F2}");
                                Checkout = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Insufficient payment");
                                continue;
                            }
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }




            //Display Receipt
            Console.WriteLine("------------------------");
            Console.WriteLine("|         Receipt       |");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Grand total:{grand_total} php");
            Console.WriteLine($"Discount: {discount} php");
            Console.WriteLine($"Final Total: {finaltotal} php");
            Console.WriteLine("------------------------");

            //Updated Data
            while (true)
            {
                Console.Write("Would you like to see the updated data(Y/N): ");
                answer = Console.ReadLine().ToUpper();

                if (answer == "Y")
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("|     Updated Stock     |");
                    Console.WriteLine("------------------------");
                    foreach (Product u in products)
                    {
                        u.DisplayProduct();
                    }
                    Console.WriteLine("------------------------");
                }
                else if (answer == "N")
                {
                    Console.WriteLine("Thank you for Using the System");
                    break;
                }

                break;
            }



            Console.ReadKey();
        }
    }

}
