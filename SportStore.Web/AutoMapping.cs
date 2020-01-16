using AutoMapper;
using SportStore.Core.Entities;
using SportStore.Web.Models.Dto;

namespace SportStore.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Order, OrderDto>(); 
            CreateMap<OrderDto, Order>(); 
        }
    }
}