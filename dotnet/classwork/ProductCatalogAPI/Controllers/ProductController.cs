using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = ProductRepository.GetAll();
            return Ok(products);//204
        }


        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProductById(int id)
        {
            var product = ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();//404
            }
            return Ok(product);//200
        }

        [HttpPost]
        public ActionResult<ProductDTO> AddProduct([FromBody] ProductDTO newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);//400
            }
            newProduct.ProductID = 0; 
            var addedProduct = ProductRepository.Add(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = addedProduct.ProductID }, addedProduct);//201
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceProduct(int id, [FromBody] ProductDTO productDetails)
        {

            if (id != productDetails.ProductID)
            {
                return BadRequest("ProductID mismatch."); // HTTP 400
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // HTTP 400
            }

            var updatedProduct = ProductRepository.Update(productDetails);

            if (updatedProduct == null)
            {
                return NotFound(); //  HTTP 404
            }

            return NoContent(); //HTTP 204

        
        }

        public class PatchProductDTO
        {
            [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than 0.")]
            public decimal? Price { get; set; }
            [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be 0 or greater.")]
            public int? StockQuantity { get; set; }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePartial(int id, [FromBody] PatchProductDTO patchData)
        {
            if(!patchData.Price.HasValue && !patchData.StockQuantity.HasValue)
            {
                return BadRequest("At least one field (Price or StockQuantity) must be provided for update.");
            }
            if ((!TryValidateModel(patchData)))
                {
                return BadRequest(ModelState);

            }
            var updatedProduct = ProductRepository.UpdatePriceOrStock(id, patchData.Price, patchData.StockQuantity);
            if (updatedProduct == null)
            {
                return NotFound(); //  HTTP 404
            }
            else
            {
                return NoContent(); //HTTP 204
            }
    }

    //public IActionResult Index()
    //{
    //    return View();
    //}
}
}
