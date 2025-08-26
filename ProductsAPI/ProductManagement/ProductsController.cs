using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Authentication.Definitions;
using ProductsAPI.Database;

namespace ProductsAPI.ProductManagement
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductDBContext productDBContext, IVatService vatService) : ControllerBase
    {
        [HttpGet("products")]
        [Authorize(Policy = Policies.GetProducts)]
        public IActionResult Get()
        {
            return Ok(ToDto(productDBContext.Product));
        }

        [HttpGet("products/{id}")]
        [Authorize(Policy = Policies.GetProducts)]
        public IActionResult GetById([FromRoute] int? id)
        {
            var dbProduct = productDBContext.Product.FirstOrDefault(prod => prod.Id == id);
            if (dbProduct == null)
                return NotFound($"Product with ID {id} not found");
            return Ok(ToDto(dbProduct));
        }

        private IEnumerable<Dtos.Product> ToDto(IEnumerable<DbObjects.Product> dbProducts)
        {
            return dbProducts.Select(ToDto);
        }

        private Dtos.Product ToDto(DbObjects.Product dbProduct)
        {
            return new Dtos.Product
            {
                ItemName = dbProduct.Title,
                Price = dbProduct.Price,
                Quantity = dbProduct.Quantity,
                TotalPriceWithVat = vatService.GetTotalPriceWithVat(dbProduct)
            };
        }

    }
}
