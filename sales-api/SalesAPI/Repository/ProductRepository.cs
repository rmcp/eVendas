using Microsoft.EntityFrameworkCore;
using SalesAPI.Data;
using SalesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private SalesContext _context;

        public ProductRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<Product> Get(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }        

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }        

        public async Task<Product> Update(Product product)
        {
                        
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return product;
            
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
