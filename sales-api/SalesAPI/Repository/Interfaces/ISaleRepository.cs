using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Repository
{
    public interface ISaleRepository : IDisposable
    {
        Task<Sale> Add(Sale sale);

        Task<Sale> Get(Guid id);

        Task<IEnumerable<Sale>> GetAll();
    }
}
