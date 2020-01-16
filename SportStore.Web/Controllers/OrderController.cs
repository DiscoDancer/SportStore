using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.BusinessLogic;
using SportStore.Core.Entities;
using SportStore.Web.Models.Dto;

namespace SportStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _repository;
        private readonly Cart _cart;
        private readonly IMapper _mapper;

        public OrderController(IRepository<Order> repoService, Cart cartService, IMapper mapper)
        {
            _repository = repoService;
            _cart = cartService;
            _mapper = mapper;
        }

        public ViewResult Checkout() => View(new OrderDto());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!_cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (!ModelState.IsValid)
            {
                return View(_mapper.Map<OrderDto>(order));
            }

            order.Lines = _cart.Lines.ToArray();
            _repository.Add(order);
            return RedirectToAction(nameof(Completed));

        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}