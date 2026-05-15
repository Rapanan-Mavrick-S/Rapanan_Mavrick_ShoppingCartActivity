using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Category
    {
        private int id;
        private string name = "";

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
    }
}


