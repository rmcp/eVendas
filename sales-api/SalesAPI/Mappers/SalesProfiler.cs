using AutoMapper;
using SalesAPI.DTO;
using SalesAPI.Models;

namespace SalesAPI.Mappers
{
    public class SalesProfiler : Profile
    {        
        public SalesProfiler()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Sale, SaleDTO>();
            CreateMap<SaleDTO, Sale>();
        }
    }
}
