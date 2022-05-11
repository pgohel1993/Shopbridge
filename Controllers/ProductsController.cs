using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Data.IConfiguration;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly IProductService productService;
        //private readonly ILogger<ProductsController> logger;

        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(ILogger<ProductsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

       
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.All();
            return Ok(products);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if (product == null)
                return BadRequest();
            return Ok(product);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                //user.Id = Guid.NewGuid();
                await _unitOfWork.Products.Add(product);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetProduct", new { product.Id }, product);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if (product == null)
                return BadRequest();

            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }

        private bool ProductExists(int id)
        {
            return false;
        }
    }
}
