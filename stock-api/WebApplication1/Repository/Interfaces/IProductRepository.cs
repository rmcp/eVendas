using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Repository
{
    public interface IProductRepository : IDisposable 
    {
        Task<Product> Add(Product product);

        Task<Product> Update(Product product);

        bool Delete(Guid id);

        Task<Product> Get(Guid id);

        Task<Product> GetByName(string name);

        Task<Product> GetByCode(int code);

        Task<IEnumerable<Product>> GetAll();

        bool ProductExists(Guid id);
    }
}
