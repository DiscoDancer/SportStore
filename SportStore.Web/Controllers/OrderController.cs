using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Checkout(OrderDto order)
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
            _repository.Add(_mapper.Map<Order>(order));
            return RedirectToAction(nameof(Completed));

        }

        [Authorize]
        public ViewResult List() =>
            View(_repository.List().Where(o => !o.Shipped).Select(_mapper.Map<OrderDto>));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int id)
        {
            var order = _repository.List()
                .FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                order.Shipped = true;
                _repository.Update(order);
            }

            return RedirectToAction(nameof(List));
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}