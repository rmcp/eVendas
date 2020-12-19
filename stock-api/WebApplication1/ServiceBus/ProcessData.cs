using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockAPI.Data;
using StockAPI.DTO;
using StockAPI.Models;
using System;

namespace StockAPI.ServiceBus
{
    public class ProcessData : IProcessData
    {
        private readonly IConfiguration _configuration;
        public ProcessData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Process(SaleRealizedMessage sale)
        {

            var connectionstring = _configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<StockContext>();
            optionsBuilder.UseSqlServer(connectionstring);
           

            using (StockContext dbContext = new StockContext(optionsBuilder.Options))
            {
                try
                {
                    var productDB = dbContext.Products.Find(sale.ProductId);

                    productDB.Amount -= sale.Amount;
                    
                    dbContext.Entry(productDB).State = EntityState.Modified;                    

                    dbContext.SaveChanges();

                } catch (Exception ex)
                {
                    throw ex;
                }
                
            }                        
            
        }
    }
}
