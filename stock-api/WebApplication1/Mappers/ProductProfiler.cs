using AutoMapper;
using StockAPI.DTO;
using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Mappers
{
    public class ProductProfiler : Profile
    {        
        public ProductProfiler()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            //CreateMap<IEnumerable<Product>, IEnumerable<ProductDTO>>();
            //CreateMap<IEnumerable<ProductDTO>, IEnumerable<Product>>();
        }
    }
}
