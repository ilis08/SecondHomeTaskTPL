using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHomeTaskTPL
{
    public class Product
    {
        public Product(int id, string name, decimal price, int count)
        {
            Id = id;
            Name = name;
            Price = price;
            Count = count;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
