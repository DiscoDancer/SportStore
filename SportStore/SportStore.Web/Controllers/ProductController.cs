using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web.Models.Dto;

namespace SportStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductController(IRepository<Product> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public ViewResult List() => View(_repository.List().Select(_mapper.Map<ProductDto>));
    }
}