using ProductCatalogAPI.Model;

namespace ProductCatalogAPI.Data
{
    public static class ProductRepository
    {
        public static List<ProductDTO> Products { get; set; } = new List<ProductDTO>
        {
            new ProductDTO { ProductID = 1, Name = "Apple iPhone 15", Category = "Electronics", Price = 999.99m, StockQuantity = 50 },
            new ProductDTO { ProductID = 2, Name = "Samsung Galaxy S23", Category = "Electronics", Price = 899.99m, StockQuantity = 35 },
            new ProductDTO { ProductID = 3, Name = "Nike Running Shoes", Category = "Footwear", Price = 120.50m, StockQuantity = 100 },
            new ProductDTO { ProductID = 4, Name = "Sony WH-1000XM5 Headphones", Category = "Electronics", Price = 350.00m, StockQuantity = 20 },
            new ProductDTO { ProductID = 5, Name = "Levi's Denim Jeans", Category = "Clothing", Price = 60.00m, StockQuantity = 75 }
        };

        // Helper to get the next ProductID for new products
        private static int NextID => Products.Any() ? Products.Max(p => p.ProductID) + 1 : 1;

        // Task 1: GET all products
        public static IEnumerable<ProductDTO> GetAll()
        {
            return Products;
        }

        // Task 2: GET product by ID
        public static ProductDTO GetById(int id)
        {
            return Products.FirstOrDefault(p => p.ProductID == id);
        }

        // Task 3: POST -> Add new product
        public static ProductDTO Add(ProductDTO newProduct)
        {
            newProduct.ProductID = NextID;
            Products.Add(newProduct);
            return newProduct;
        }

        // Task 4: PUT -> Replace product details completely
        public static ProductDTO Update(ProductDTO updatedProduct)
        {
            var existingProduct = Products.FirstOrDefault(p => p.ProductID == updatedProduct.ProductID);
            if (existingProduct != null)
            {
                // Update all fields
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.StockQuantity = updatedProduct.StockQuantity;
            }
            return existingProduct;
        }

        // Helper method for PATCH
        public static ProductDTO UpdatePriceOrStock(int id, decimal? price, int? stockQuantity)
        {
            var existingProduct = Products.FirstOrDefault(p => p.ProductID == id);
            if (existingProduct != null)
            {
                if (price.HasValue)
                {
                    existingProduct.Price = price.Value;
                }
                if (stockQuantity.HasValue)
                {
                    existingProduct.StockQuantity = stockQuantity.Value;
                }
            }
            return existingProduct;
        }
    }
}
