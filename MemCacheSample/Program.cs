using System;
using System.Net;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace MemCacheSample
{
    class Program
    {
        [Serializable]
        class Product
        {
            public double Price;
            public string Name;

            public override string ToString()
            {
                return string.Format("Product {{{0}: {1}}}", Name, Price);
            }
        }

        static void Main(string[] args)
        {
            int port = 11211;
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            config.Protocol = MemcachedProtocol.Binary;
          
            var memcachedClient = new MemcachedClient(config);

            // Set Product Info
            memcachedClient.Store(StoreMode.Set, "one", 
                    new Product
                    {
                        Price  = 1.24,
                        Name = "Mineral Water"
                    }, 
                    new TimeSpan(0, 10, 0));
            
            // Get Product Info
            var product = memcachedClient.Get<Product>("one");

            Console.WriteLine("Get product information...");
            Console.WriteLine("Name: {0}", product.Name);
            Console.WriteLine("Price: {0}", product.Price);

            // Remove all data from the Cache
            memcachedClient.FlushAll();

            Console.ReadLine();
        }
    }
}
