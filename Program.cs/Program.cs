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
            Category fruit = new Category { ID = 1, Name = "Fruits" };
            Category vegetable = new Category { ID = 2, Name = "Vegetables" };
            Category tools = new Category { ID = 3, Name = "Tools" };


            //Store Display Menu
            Product[] products = new Product[10];

            products[0] = new Product { ID = 1, Name = "Apple", Price = 10, RemainingStock = 15, Category = "Fruits" };
            products[1] = new Product { ID = 2, Name = "Banana", Price = 12, RemainingStock = 10, Category = "Fruits" };
            products[2] = new Product { ID = 3, Name = "Orange", Price = 13.25, RemainingStock = 12, Category = "Fruits" };
            products[3] = new Product { ID = 4, Name = "Onions", Price = 7, RemainingStock = 15, Category = "Vegetables" };
            products[4] = new Product { ID = 5, Name = "Golden Shovel", Price = 5000, RemainingStock = 10, Category = "Tools" };
            products[5] = new Product { ID = 6, Name = "Trowel", Price = 300, RemainingStock = 20, Category = "Tools" };
            products[6] = new Product { ID = 7, Name = "Shovel", Price = 500, RemainingStock = 15, Category = "Tools" };
            products[7] = new Product { ID = 8, Name = "Cabbage", Price = 100, RemainingStock = 30, Category = "Vegetables" };
            products[8] = new Product { ID = 9, Name = "Carrots", Price = 90, RemainingStock = 30, Category = "Vegetables" };
            products[9] = new Product { ID = 10, Name = "Tomatoes", Price = 50, RemainingStock = 30, Category = "Vegetables" };



            //Cart
            Product[] cart = new Product[3];
            int[] cart_quantity = new int[3];
            int cart_count = 0;

            //Order History
            Order[] history = new Order[10];
            int historyCount = 0;
            int receiptCounter = 1;

            String answer;
            bool outer = true;
            double grand_total = 0;
            double finaltotal = 0;
            double discount = 0;
            double payment;
            bool Checkout = true;
            bool lowStock = false;
            String exit;


            do
            {

                //User input
                int choice;
                int quantity;

                Console.WriteLine("-----------------------");
                Console.WriteLine("|Shopping Cart System |");
                Console.WriteLine("-----------------------");
                foreach (Product i in products)
                {
                    i.DisplayProduct();
                }
                Console.WriteLine("------------------------");
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

                //Add products to the cart and checks if the cart is full
                if (!found)
                {
                    if (cart_count >= cart.Length)
                    {
                        Console.WriteLine("Cart is Full");
                        goto cartfull;
                    }

                    cart[cart_count] = selected;
                    cart_quantity[cart_count] = quantity;
                    cart_count++;

                }

                selected.DeductStock(quantity);
                Console.WriteLine($"{selected.Name} x{quantity} was added to cart");

                //Asking the users if they want to continue
            cartfull:
                while (true)
                {
                    Console.Write("Add Item (Y/N)?: ");
                    answer = Console.ReadLine().ToUpper();

                    if (answer == "Y")
                    {
                        Checkout = false;
                        break;
                    }
                    else if (answer == "N")
                    {
                        Checkout = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        continue;
                    }
                }

                while (Checkout)
                {
                    int choice2;

                    Console.WriteLine("\n-----------------------");
                    Console.WriteLine("|      Cart Menu      |");
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("1. Add item");
                    Console.WriteLine("2. View Cart");
                    Console.WriteLine("3. Remove an item from cart");
                    Console.WriteLine("4. Update item quantity");
                    Console.WriteLine("5. Clear Cart");
                    Console.WriteLine("6. Checkout");
                    Console.WriteLine("7. View Order History");
                    Console.WriteLine("8. Product Search");
                    Console.WriteLine("9. Exit");

                    Console.Write("\nEnter Your choice: ");

                    if (!int.TryParse(Console.ReadLine(), out choice2))
                    {
                        Console.WriteLine("Invalid input! Please enter a number.");
                        continue;
                    }

                    if (choice2 < 1 || choice2 > 9)
                    {
                        Console.WriteLine("Please enter a number from 1-9.");
                        continue;
                    }

                    switch (choice2)
                    {
                        case 1:
                            Checkout = false;
                            break;

                        case 2:

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

                        case 3:

                            //check if you cart is empty
                            if (cart_count == 0)
                            {
                                Console.WriteLine("Your Cart is Empty!");
                                break;
                            }

                            Console.Write("Which item to remove: ");

                            int removeItem = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (removeItem < 0 || removeItem >= cart_count)
                            {
                                Console.WriteLine("Invalid Item!");
                                break;
                            }
                            
                            //restock the item
                            cart[removeItem].RemainingStock += cart_quantity[removeItem];

                            //removes the item
                            for (int i = removeItem; i < cart_count - 1; i++)
                            {
                                cart[i] = cart[i + 1];
                                cart_quantity[i] = cart_quantity[i + 1];
                            }


                            cart[cart_count - 1] = null;
                            cart_quantity[cart_count - 1] = 0;

                            cart_count--;

                            Console.WriteLine("Item successfully removed!");
                            break;

                        case 4:

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

                            Console.Write("How many do you want to add?: ");
                            int addQuantity = Convert.ToInt32(Console.ReadLine());

                            if (addQuantity <= 0)
                            {
                                Console.WriteLine("Enter a number greater than 0");
                                break;
                            }

                            Product item = cart[updateitem];

                            if (!item.HasEnoughStock(addQuantity))
                            {
                                Console.WriteLine("Not enough stock!");
                                break;
                            }

                            item.DeductStock(addQuantity);
                            cart_quantity[updateitem] += addQuantity;

                            Console.WriteLine("Quantity updated successfully!");
                            break;

                        case 5:

                            //check if you cart is empty
                            if (cart_count == 0)
                            {
                                Console.WriteLine("Your Cart is Empty!");
                                break;
                            }

                            //restock the item before clearing
                            for (int i = 0; i < cart_count; i++)
                            {
                                cart[i].RemainingStock += cart_quantity[i];
                            }

                            cart_count = 0;
                            Console.WriteLine("\nCart is Cleared");
                            break;

                        case 6:

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

                            Console.WriteLine($"\nFinal Total = php {finaltotal}");

                            while (true)
                            {
                                //validate the payment input
                                Console.Write("Enter payment: php ");
                                if (!double.TryParse(Console.ReadLine(), out payment) || payment <= -1)
                                {
                                    Console.WriteLine("Invalid Amount");
                                    continue;
                                }

                                if (payment >= finaltotal)
                                {
                                    Console.WriteLine($"\nFinal Total: php{finaltotal:F2}");
                                    Console.WriteLine($"Payment: php{payment:F2}");
                                    Console.WriteLine($"Change: php{payment - finaltotal:F2}");

                                    string receiptNo = receiptCounter.ToString("D4");
                                    DateTime now = DateTime.Now;
                                    double change = payment - finaltotal;

                                    Console.WriteLine("\n------------------------");
                                    Console.WriteLine("|        RECEIPT       |");
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine($"Receipt No: {receiptNo}");
                                    Console.WriteLine($"Date: {now}");
                                    Console.WriteLine("------------------------");

                                    for (int i = 0; i < cart_count; i++)
                                    {
                                        Console.WriteLine($"{cart[i].Name} x{cart_quantity[i]}");
                                    }

                                    Console.WriteLine("------------------------");
                                    Console.WriteLine($"Grand Total: PHP {grand_total:F2}");
                                    Console.WriteLine($"Discount: PHP {discount:F2}");
                                    Console.WriteLine($"Final Total: PHP {finaltotal:F2}");
                                    Console.WriteLine($"Payment: PHP {payment:F2}");
                                    Console.WriteLine($"Change: PHP {change:F2}");
                                    Console.WriteLine("------------------------");

                                    history[historyCount] = new Order
                                    {
                                        ReceiptNo = receiptNo,
                                        Date = now,
                                        FinalTotal = finaltotal,
                                        Payment = payment,
                                        Change = change
                                    };

                                    historyCount++;
                                    receiptCounter++;

                                    grand_total = 0;
                                    discount = 0;

                                    cart_count = 0;

                                    foreach (Product p in products)
                                    {
                                        if (p.RemainingStock <= 5)
                                        {
                                            lowStock = true;
                                            break;
                                        }
                                    }

                                    if (lowStock)
                                    {
                                        Console.WriteLine("\nLOW STOCK ALERT:");

                                        foreach (Product p in products)
                                        {
                                            if (p.RemainingStock <= 5)
                                            {
                                                Console.WriteLine($"{p.Name} has only {p.RemainingStock} left.");
                                            }
                                        }
                                    }

                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient payment");
                                    continue;
                                }

                            }

                            break;

                        case 7:

                            if (historyCount == 0)
                            {
                                Console.WriteLine("No orders yet.");
                                break;
                            }

                            Console.WriteLine("\n------------------------");
                            Console.WriteLine("|    ORDER HISTORY     |");
                            Console.WriteLine("------------------------");

                            for (int i = 0; i < historyCount; i++)
                            {
                                Console.WriteLine($"Receipt #{history[i].ReceiptNo}");
                                Console.WriteLine($"Date: {history[i].Date}");
                                Console.WriteLine($"Final Total: PHP {history[i].FinalTotal:F2}");
                                Console.WriteLine("------------------------");
                            }

                            break;

                        case 8:
                            Console.WriteLine("Categories:");
                            Console.WriteLine("- Fruits");
                            Console.WriteLine("- Vegetables");
                            Console.WriteLine("- Tools");
                            Console.Write("Enter category to search: ");
                            string searchCategory = Console.ReadLine() ?? "";

                            bool foundCategory = false;

                            // First, check if the category exists
                            foreach (Product p in products)
                            {
                                if (p.Category.ToLower() == searchCategory.ToLower())
                                {
                                    foundCategory = true;
                                    break;
                                }
                            }

                            if (!foundCategory)
                            {
                                Console.WriteLine("Category not found.");
                                break;
                            }

                            // Show all products in that category
                            Console.WriteLine($"\nProducts in '{searchCategory}':");
                            foreach (Product p in products)
                            {
                                if (p.Category.ToLower() == searchCategory.ToLower())
                                {
                                    Console.WriteLine($"- {p.Name}");
                                }
                            }

                            // Enter product name
                            Console.Write("\nEnter product name to search: ");
                            string searchName = Console.ReadLine() ?? "";

                            bool foundItem = false;

                            Console.WriteLine("\nProduct Details:");
                            foreach (Product p in products)
                            {
                                if (p.Category.ToLower() == searchCategory.ToLower() &&
                                    p.Name.ToLower() == searchName.ToLower())
                                {
                                    p.DisplayProduct();
                                    foundItem = true;
                                    break;
                                }
                            }

                            if (!foundItem)
                            {
                                Console.WriteLine($"No product named '{searchName}' found in {searchCategory}.");
                            }

                            break;

                        case 9:
                            while (true)
                            {
                                Console.Write("\nDo you really want to exit Y/N? ");
                                exit = Console.ReadLine().ToUpper();

                                if (exit == "Y")
                                {
                                    Console.WriteLine("Thank you for Using The Shopping System!!");
                                    outer = false;
                                    Checkout = false;
                                    break;
                                }
                                else if (exit == "N")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    continue;
                                }
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }

            } while (outer);


            Console.ReadKey();
        }
    }


}
