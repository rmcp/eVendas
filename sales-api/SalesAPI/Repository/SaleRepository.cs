using Microsoft.EntityFrameworkCore;
using SalesAPI.Data;
using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SalesAPI.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private SalesContext _context;

        public SaleRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<Sale> Add(Sale sale)
        {
            using (var t = _context.Database.BeginTransaction())
            {                

                try
                {

                    var newProductsList = new List<Product>();

                    var newSale = new Sale();

                    foreach (var product in sale.Products)
                    {
                        var productDb = _context.Products.Find(product.Id);

                        productDb.Amount -= product.Amount;

                        newSale.Total += product.Amount * productDb.Price;                        

                        _context.Entry(productDb).State = EntityState.Modified;                    
                    }

                    newSale.Date = DateTime.Now;                                       

                    _context.Sales.Add(newSale);

                    await _context.SaveChangesAsync();

                    await t.CommitAsync();

                    return sale;

                } catch (Exception ex)
                {
                    t.Rollback();
                    throw;
                }               

            }

        }

        public async Task<Sale> Get(Guid id)
        {
            return await _context.Sales.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sale>> GetAll()
        {
            return await _context.Sales.ToListAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
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
