using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web.Models.Dto;

namespace SportStore.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public AdminController(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ViewResult Index() => View(_repository.List().Select(_mapper.Map<ProductDto>));
    }
}