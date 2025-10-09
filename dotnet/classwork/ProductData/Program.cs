//namespace ProductData
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello, World!");
//        }
//    }
//}

using ProductData;
var products = new List<Product>
            {
                new Product{ Id=1, Name="Laptop", Category="Electronics", Price=75000, Stock=15 },
                new Product{ Id=2, Name="Smartphone", Category="Electronics", Price=55000, Stock=25 },
                new Product{ Id=3, Name="Tablet", Category="Electronics", Price=30000, Stock=10 },
                new Product{ Id=4, Name="Headphones", Category="Accessories", Price=2000, Stock=100 },
                new Product{ Id=5, Name="Shirt", Category="Fashion", Price=1500, Stock=50 },
                new Product{ Id=6, Name="Jeans", Category="Fashion", Price=2200, Stock=30 },
                new Product{ Id=7, Name="Shoes", Category="Fashion", Price=3500, Stock=20 },
                new Product{ Id=8, Name="Refrigerator", Category="Appliances", Price=45000, Stock=8 },
                new Product{ Id=9, Name="Washing Machine", Category="Appliances", Price=38000, Stock=6 },
                new Product{ Id=10, Name="Microwave", Category="Appliances", Price=12000, Stock=12 }
            };

//Q1:
//var allProducts = products.Where(products => products.Stock <= 20);

//foreach (var name in allProducts)
//{
//    Console.WriteLine(name.Name);
//}

//Q2:
//var allProducts = products.Where(products => products.Category == "Fashion");

//foreach (var name in allProducts)
//{
//    Console.WriteLine(name.Name);
//}

//Q3:
//var allProducts = products.Where(products => products.Price >= 10000);

//foreach (var name in allProducts)
//{
//    Console.WriteLine($"{name.Name}--{name.Price}");
//}

//Q4:
var allProducts = products.OrderByDescending(products => products.Price);
foreach (var name in allProducts)
{
    Console.WriteLine($"{name.Name}--{name.Price}");
}

/*Product Tasks
 
1.Display all products with stock less than 20.
 
2. Show all products belonging to the “Fashion” category.
 
3. Display product names and prices where price is greater than 10,000.
 
4. List all product names sorted by price (descending).
*/