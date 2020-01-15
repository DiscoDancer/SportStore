using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web.Models.Dto;
using SportStore.Web.Models.ViewModels;

namespace SportStore.Web.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 4;

        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductController(IRepository<Product> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public ViewResult List(string category, int productPage = 1)
        {
            var products = _repository
                .List()
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.Id)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize)
                .Select(_mapper.Map<ProductDto>);

            var pagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                    _repository.List().Count() :
                    _repository.List().Count(e => e.Category == category)
            };

            return View(new ProductsListViewModel
            {
                Products = products,
                PagingInfo = pagingInfo,
                CurrentCategory = category
            });
        } 

    }
}