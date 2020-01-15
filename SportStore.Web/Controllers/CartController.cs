using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.Entities;
using SportStore.Web.Extensions;
using SportStore.Web.Models.ViewModels;

namespace SportStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _repository;
        public CartController(IRepository<Product> repo)
        {
            _repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var product = _repository.List()
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId,
            string returnUrl)
        {
            var product = _repository.List()
                .FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        private Cart GetCart() => HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}