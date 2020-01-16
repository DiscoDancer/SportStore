using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.BusinessLogic;
using SportStore.Core.Entities;
using SportStore.Web.Extensions;
using SportStore.Web.Models.ViewModels;

namespace SportStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly Cart _cart;

        public CartController(IRepository<Product> repo, Cart cartService)
        {
            _repository = repo;
            _cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var product = _repository.List()
                .FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int id,
            string returnUrl)
        {
            var product = _repository.List()
                .FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}