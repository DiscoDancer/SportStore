using AutoMapper;
using SportStore.Core.Entities;
using SportStore.Web.Models.Dto;

namespace SportStore.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>(); // means you want to map from User to UserDTO
        }
    }
}