using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesAPI.DTO;


namespace SalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            try
            {
                var products = await _productService.GetAll();

                return products.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao obter os produtos à venda");
                return StatusCode(500, "Erro ao obter os produtos à venda");
            }            
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(Guid id)
        {
            try
            {
                
                var product = await _productService.Get(id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao obter os calculos de impostos dos contribuintes informados");
                return StatusCode(500, "Erro ao obter os calculos de impostos dos contribuintes informados");
            }
        }        
        
    }
}
