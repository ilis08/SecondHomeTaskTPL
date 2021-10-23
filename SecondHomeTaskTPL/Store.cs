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

        private int SoldItems { get; set; }
        private int SupplyItems { get; set; }

        private object lockerForAdd = new();
        private object lockerForBuy = new();

        public void AddProducts(int countOfProducts, int countOfCertainProduct)
        {            
            lock (lockerForAdd)
            {
                for (int i = 0; i < countOfProducts; i++)
                {
                    Random r = new();

                    int id = r.Next(1, 6);

                    var prod = Products.Where(c => c.Id == id).FirstOrDefault();
                  
                    foreach (var product in Products.Where(c => c.Id == prod.Id))
                    {
                        product.Count += countOfCertainProduct;
                    }

                    Console.WriteLine($"Supplier add a {countOfCertainProduct} to {prod.Name}");
                    SupplyItems += countOfCertainProduct;
                }
            }
        }

        public void BuyProduct(int countOfProducts, int countOfCertainProduct)
        {
            lock (lockerForBuy)
            {
                for (int i = 0; i < countOfProducts; i++)
                {
                    Random r = new Random();

                    int id = r.Next(1, 6);

                    var product = Products.Where(c => c.Id == id).FirstOrDefault();

                    if (product.Count >= countOfCertainProduct)
                    {
                        foreach (var item in Products.Where(c => c.Id == product.Id))
                        {
                            item.Count -= countOfCertainProduct;
                            Console.WriteLine($"{countOfCertainProduct} items of {product.Name} was bought!");
                            SoldItems += countOfCertainProduct;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Product {product.Name} is not available, please try later");
                    }
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

        public void Statistic()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Supplied items: {SupplyItems}");
            Console.WriteLine($"Sold items : {SoldItems}");
        }
    }
}
