using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesAPI.Data;
using SalesAPI.DTO;
using SalesAPI.Mappers;
using SalesAPI.Models;
using SalesAPI.Repository;
using SalesAPI.Services;
using System;

namespace SalesAPI.ServiceBus
{
    public class ProcessData : IProcessData
    {
        private readonly IConfiguration _configuration;
        public ProcessData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Process(ProductDTO productDto)
        {

            var connectionstring = _configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();
            optionsBuilder.UseSqlServer(connectionstring);
           

            using (SalesContext dbContext = new SalesContext(optionsBuilder.Options))
            {
                try
                {
                    var productDB = dbContext.Products.Find(productDto.Id);

                    if (productDB == null)
                    {
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
                        var mapper = config.CreateMapper();

                        var product = mapper.Map<Product>(productDto);

                        dbContext.Products.Add(product);
                    }
                    else
                    {
                        productDB.Amount = productDto.Amount;
                        productDB.Price = productDto.Price;
                        dbContext.Entry(productDB).State = EntityState.Modified;
                    }

                    dbContext.SaveChanges();
                } catch (Exception ex)
                {
                    throw ex;
                }
                
            }                        
            
        }
    }
}
