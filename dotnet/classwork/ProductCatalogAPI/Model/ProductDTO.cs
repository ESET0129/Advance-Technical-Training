using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Model
{
    public class ProductDTO
    {
       
        public int ProductID { get; set; }

         
        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Category is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category must be between 2 and 50 characters.")]
        public string Category { get; set; }

        
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        
        [Required(ErrorMessage = "Stock Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be 0 or greater.")]
        public int StockQuantity { get; set; }
    }

}






