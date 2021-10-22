using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHomeTaskTPL
{
    public class Store
    {
        private List<Product> Products = new()
        {
            new Product(1, "Laptop", 1000, 100),
            new Product(2, "Phone", 500, 100),
            new Product(3, "TV", 800, 100),
            new Product(4, "Fridge", 2000, 100),
            new Product(5, "Washer", 1300, 100)
        };

        private object lockerForAdd = new();
        private object lockerForBuy = new();

        public void AddProducts(int id, int count)
        {
            
            lock (lockerForAdd)
            {
                Console.WriteLine("-----------AddProduct----------");

                var prod = Products.Where(c => c.Id == id).FirstOrDefault();

                Console.WriteLine($"Supplier add a {count} to {prod.Name}");

                foreach (var product in Products.Where(c => c.Id == id))
                {
                    product.Count += count;
                }

                Console.WriteLine("-------------------------------");
            }
        }

        public bool BuyProduct(int id, int count)
        {
            lock (lockerForAdd)
            {
                Console.WriteLine("----------BuyProduct-----------");

                var product = Products.Find(c => c.Id == id);

                if (product.Count >= count)
                {
                    foreach (var item in Products.Where(c => c.Id == id))
                    {
                        item.Count -= count;
                        Console.WriteLine($"{count} items of  {product.Name} was bought!");
                        Console.WriteLine("-------------------------------");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine($"Product {product.Name} is not available, please try later");
                    Console.WriteLine("-------------------------------");
                    return false;
                }

                
            }          
        }

        public void GetProductsStates()
        {
            Console.WriteLine("----------------------------");
            foreach (var item in Products)
            {
                Console.WriteLine($"Product {item.Name} is {item.Count} item available");
            }
        }
    }
}
