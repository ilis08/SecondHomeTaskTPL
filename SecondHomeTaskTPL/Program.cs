using System;
using System.Collections.Generic;
using System.Threading;

namespace SecondHomeTaskTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();

            List<Thread> suppliers = new();
            List<Thread> buyers = new();

            Random r = new();

            for (int i = 0; i < 5; i++)
            {
                suppliers.Add(new Thread(() => { store.AddProducts(r.Next(1, 6), r.Next(1, 300)); }));
                suppliers[i].Start();
            }

            for (int i = 0; i < 100; i++)
            {
                buyers.Add(new Thread(() => { store.BuyProduct(r.Next(1, 6), r.Next(1, 21)); }));
                buyers[i].Start();
            }

            for (int i = 0; i < suppliers.Count; i++)
            {
                suppliers[i].Join();
            }

            for (int i = 0; i < buyers.Count; i++)
            {
                buyers[i].Join();
            }

            store.GetProductsStates();
        }
    }
}
