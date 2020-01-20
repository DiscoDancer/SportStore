using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web.Models.Dto;

namespace SportStore.Web.Controllers
{
    [Authorize]
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

        public ViewResult Edit(int id)
        {
            var product = _repository.GetById(id);
            var productDto = product == null ? null : _mapper.Map<ProductDto>(product);

            return View(productDto);
        }

        // for update and add
        [HttpPost]
        public IActionResult Edit(ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (product.Id != 0)
            {
                var foundProduct = _repository.GetById(product.Id);
                if (foundProduct == null)
                {
                    throw new ArgumentException();
                }
                _mapper.Map(product, foundProduct);
                _repository.Update(foundProduct);
            }
            else
            {
                _repository.Add(_mapper.Map<Product>(product));
            }


            TempData["message"] = $"{product.Name} has been saved";

            return RedirectToAction("Index");

        }

        public ViewResult Create() => View("Edit", new ProductDto());

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product != null)
            {
                _repository.Delete(product);
                TempData["message"] = $"{product.Name} was deleted";
            }

            return RedirectToAction("Index");
        }
    }
}