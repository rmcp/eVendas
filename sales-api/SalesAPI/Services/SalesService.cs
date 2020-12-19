using AutoMapper;
using FluentValidation.Results;
using SalesAPI.DTO;
using SalesAPI.Models;
using SalesAPI.Models.Validators;
using SalesAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public SaleService(ISaleRepository saleRepository, IProductService productservice, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productService = productservice;
            _mapper = mapper;
        }
       
        public async Task<SaleDTO> Get(Guid id)
        {

            var sale = await _saleRepository.Get(id);

            if (sale != null)
            {
                return _mapper.Map<SaleDTO>(sale);
            }

            return null;
        }

        public async Task<IEnumerable<SaleDTO>> GetAll()
        {
            var sales = await _saleRepository.GetAll();

            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task<SaleDTO> Sell(SaleDTO saleDTO)
        {
            var sale = _mapper.Map<Sale>(saleDTO);

            await Validate(sale);            

            return _mapper.Map<SaleDTO>(await _saleRepository.Add(sale));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> Validate(Sale sale)
        {

            var saleValidator = new SaleValidator();
            ValidationResult result = saleValidator.Validate(sale);

            if (!result.IsValid)
            {
                throw new BusinessException(String.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage)));
            }

            foreach (var product in sale.Products)
            {
                var productDb = await _productService.Get(product.Id);

                if (productDb == null)
                {
                    throw new BusinessException($"Produto {product.Name} não existente");
                }
            }            

            return true;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _saleRepository.Dispose();
            }

        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
