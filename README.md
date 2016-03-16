# MemCacheSample

A small application to demonstrate the usage of Memcached

# Introdution - Walkthrough to MemCached
http://deanhume.com/home/blogpost/memcached-for-c----a-walkthrough/62

# Install Memcached as a Windows Service
http://latebound.blogspot.com/2008/10/running-memcached-on-windows.html

# Memcached Client
https://github.com/enyim/EnyimMemcached

Creating a MemCached Client

    MemcachedClientConfiguration config = new MemcachedClientConfiguration();
    config.Servers.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11211));
    config.Protocol = MemcachedProtocol.Binary;
    var memcachedClient = new MemcachedClient(config);

Serialize Object to Store

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
 
Set Product Info
 
    memcachedClient.Store(StoreMode.Set, "one", 
        new Product
        {
          Price  = 1.24,
          Name = "Mineral Water"
        }, new TimeSpan(0, 10, 0));

Get Product Info
  
    var product = memcachedClient.Get<Product>("one");
