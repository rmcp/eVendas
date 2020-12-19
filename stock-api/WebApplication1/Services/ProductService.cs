using AutoMapper;
using FluentValidation.Results;
using StockAPI.DTO;
using StockAPI.Models;
using StockAPI.Models.Validators;
using StockAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public void Delete(Guid id)
        {
            _productRepository.Delete(id);
        }        

        public async Task<ProductDTO> Get(Guid id)
        {

            var product = await _productRepository.Get(id);

            if (product != null)
            {
                return _mapper.Map<ProductDTO>(product);
            }


            return null;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var products = await _productRepository.GetAll();
                       
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
           
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            
            var product = _mapper.Map<Product>(productDTO);

            var productValidator = new ProductValidator();
            ValidationResult result = productValidator.Validate(product);

            if (!result.IsValid)
            {
                throw new BusinessException(String.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage)));
            }

            return _mapper.Map<ProductDTO>(await _productRepository.Update(product));
            
        }

        public async Task<ProductDTO> Add(ProductDTO productDTO)
        {

            var product = _mapper.Map<Product>(productDTO);            

            await Validate(product);

            return _mapper.Map<ProductDTO>(await _productRepository.Add(product));

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productRepository.Dispose();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> Validate(Product product)
        {
                      
            var productValidator = new ProductValidator();
            ValidationResult result = productValidator.Validate(product);

            if (!result.IsValid)
            {
                throw new BusinessException(String.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage)));
            }

            var productDb = await _productRepository.GetByCode(product.Code);

            if (productDb != null)
            {
                throw new BusinessException("Código já existente");
            }

            productDb = await _productRepository.GetByName(product.Name);

            if (productDb != null)
            {
                throw new BusinessException("Produto de mesmo nome já existente");
            }

            return true;            
            
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
