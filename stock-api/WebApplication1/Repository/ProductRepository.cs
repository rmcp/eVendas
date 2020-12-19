using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private StockContext _context;

        public ProductRepository(StockContext context)
        {
            _context = context;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }        

        public async Task<Product> Get(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync<Product>(p => p.Name.Equals(name));
        }

        public async Task<Product> GetByCode(int code)
        {
            return await _context.Products.FirstOrDefaultAsync<Product>(p => p.Code == code);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Add(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
                        
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return product;
            
        }    
        
        public bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
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
