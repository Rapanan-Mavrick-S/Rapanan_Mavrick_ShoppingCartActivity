using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
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
