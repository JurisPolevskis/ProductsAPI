using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Authentication.Definitions;
using ProductsAPI.Database;

namespace ProductsAPI.ProductManagement
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductDBContext productDBContext, IVatService vatService, IProductValidationService validationService) : ControllerBase
    {
        [HttpGet("products")]
        [Authorize(Policy = Policies.GetProducts)]
        public IActionResult Get()
        {
            return Ok(ToDto(productDBContext.Product));
        }

        [HttpGet("products/{id}")]
        [Authorize(Policy = Policies.GetProducts)]
        public IActionResult GetById([FromRoute] int id)
        {
            var dbProduct = productDBContext.Product.FirstOrDefault(prod => prod.Id == id);
            if (dbProduct == null)
                return NotFound($"Product with ID {id} not found");
            return Ok(ToDto(dbProduct));
        }

        [HttpPost("products")]
        [Authorize(Policy = Policies.EditProducts)]
        public IActionResult Post(Dtos.Product product)
        {
            if (!validationService.Validate(product, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }
            var dbProduct = ToDb(product);
            productDBContext.Product.Add(dbProduct);
            productDBContext.SaveChanges();
            return Created("products", ToDto(dbProduct));
        }

        [HttpPut("products/{id}")]
        [Authorize(Policy = Policies.EditProducts)]
        public IActionResult Put([FromRoute] int id, Dtos.Product product)
        {
            var dbProduct = productDBContext.Product.FirstOrDefault(prod => prod.Id == id);
            if (dbProduct == null)
                return NotFound($"Product with ID {id} not found");

            if (!validationService.Validate(product, out string errorMessage))
            {
                return BadRequest(errorMessage);
            }

            dbProduct.Title = product.ItemName;
            dbProduct.Quantity = product.Quantity;
            dbProduct.Price = product.Price;

            productDBContext.SaveChanges();
            return Created("products", ToDto(dbProduct));
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

        private static DbObjects.Product ToDb(Dtos.Product dtoProduct)
        {
            return new DbObjects.Product
            {
                Title = dtoProduct.ItemName,
                Price = dtoProduct.Price,
                Quantity = dtoProduct.Quantity
            };
        }

    }
}
