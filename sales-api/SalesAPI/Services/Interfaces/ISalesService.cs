using SalesAPI.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesAPI.Services
{
    public interface ISaleService : IDisposable
    {
        Task<IEnumerable<SaleDTO>> GetAll();

        Task<SaleDTO> Get(Guid id);

        Task<SaleDTO> Sell(SaleDTO product);        

    }
}
