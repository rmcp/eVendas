using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Repository
{
    public interface IProductRepository : IDisposable 
    {
        Task<Product> Add(Product product);

        Task<Product> Update(Product product);
     
        Task<Product> Get(Guid id);

        Task<IEnumerable<Product>> GetAll();
        
    }
}
