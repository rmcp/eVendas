using StockAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Services
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<ProductDTO>> GetAll();

        Task<ProductDTO> Get(Guid id);

        Task<ProductDTO> Add(ProductDTO product);

        Task<ProductDTO> Update(ProductDTO product);

        void Delete(Guid id);

    }
}
