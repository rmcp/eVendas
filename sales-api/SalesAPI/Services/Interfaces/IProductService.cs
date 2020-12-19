using SalesAPI.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface IProductService : IDisposable
    {

        Task<ProductDTO> Add(ProductDTO product);

        Task<IEnumerable<ProductDTO>> GetAll();

        Task<ProductDTO> Get(Guid id);        

        Task<ProductDTO> Update(ProductDTO product);
        
        

    }
}
