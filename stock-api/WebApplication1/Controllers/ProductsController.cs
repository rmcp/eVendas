using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockAPI.DTO;
using Microsoft.EntityFrameworkCore;
using StockAPI.ServiceBus;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IServiceBusSender _serviceBusSender;

        public ProductsController(IProductService productService, IServiceBusSender serviceBusSender, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
            _serviceBusSender = serviceBusSender;
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
                _logger.LogCritical(ex, "Erro ao obter os produtos do estoque");
                return StatusCode(500, "Erro ao obter os produtos do estoque");
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

        // POST api/<ProductsController>
        [HttpPost]        
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO product)
        {
            try
            {
                product = await _productService.Add(product);

                await _serviceBusSender.SendMessage(product);

                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
            catch (BusinessException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao inserir o produto.");
                return StatusCode(500, "Erro ao inserir o produto.");
            }
            
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> Put(Guid id, ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }            

            try
            {
                product = await _productService.Update(product);

                await _serviceBusSender.SendMessage(product);

                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
            catch (BusinessException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            } catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao obter os calculos de impostos dos contribuintes informados");
                return StatusCode(500, "Erro ao obter os calculos de impostos dos contribuintes informados");
            }

        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(Guid id)
        {
            try
            {
                var product = await _productService.Get(id);

                if (product == null)
                {
                    return NotFound();
                }

                _productService.Delete(id);

                return product;
            } catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao obter os calculos de impostos dos contribuintes informados");
                return StatusCode(500, "Erro ao obter os calculos de impostos dos contribuintes informados");
            }
            
        }

        private bool ProductExists(Guid id)
        {
            return false;
        }
    }
}
