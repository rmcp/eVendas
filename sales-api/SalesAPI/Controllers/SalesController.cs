using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesAPI.Services;
using SalesAPI.DTO;
using SalesAPI.ServiceBus;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {

        private readonly ISaleService _saleService;
        private readonly ILogger<SalesController> _logger;
        private readonly IServiceBusSender _serviceBusSender;

        public SalesController(ISaleService saleService, IServiceBusSender serviceBusSender, ILogger<SalesController> logger)
        {
            _saleService = saleService;
            _logger = logger;
            _serviceBusSender = serviceBusSender;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> Get()
        {
            try
            {
                var products = await _saleService.GetAll();

                return products.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao obter as vendas");
                return StatusCode(500, "Erro ao obter as vendas");
            }            
        }

        // GET api/<SalesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> Get(Guid id)
        {
            try
            {
                
                var sale = await _saleService.Get(id);

                if (sale == null)
                {
                    return NotFound();
                }

                return sale;
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao obter a venda");
                return StatusCode(500, "Erro ao obter a venda");
            }
        }

        // POST api/<SalesController>
        [HttpPost]   
        [Route("api/[controller]/sell")]
        public async Task<ActionResult<SaleDTO>> Post(SaleDTO saleDTO)
        {
            try
            {
                var sale = await _saleService.Sell(saleDTO);

                foreach(var product in saleDTO.Products)
                {
                    await _serviceBusSender.SendMessage(new SaleRealizedMessage { ProductId = product.Id, Amount = product.Amount });
                }

                return CreatedAtAction(nameof(Get), new { id = sale.Id }, sale);
            }
            catch (BusinessException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Erro ao realizar a venda.");
                return StatusCode(500, "Erro ao realizar a venda.");
            }
            
        }        

    }
}
