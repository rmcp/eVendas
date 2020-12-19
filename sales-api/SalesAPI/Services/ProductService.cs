using AutoMapper;
using SalesAPI.DTO;
using SalesAPI.Models;
using SalesAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
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

        public async Task<ProductDTO> Add(ProductDTO productDTO)
        {

            var product = _mapper.Map<Product>(productDTO);            

            return _mapper.Map<ProductDTO>(await _productRepository.Add(product));

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

            return _mapper.Map<ProductDTO>(await _productRepository.Update(product));
            
        }        

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productRepository.Dispose();
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
