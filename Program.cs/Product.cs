using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Product
    {
        private int id;
        private string name = "";
        private double price;
        private int remainingStock;
        private string category = "";

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                {
                    price = value;
                }
            }
        }

        public int RemainingStock
        {
            get { return remainingStock; }
            set
            {
                if (value >= 0)
                {
                    remainingStock = value;
                }
            }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

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
