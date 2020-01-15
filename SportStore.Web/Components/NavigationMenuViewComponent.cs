using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Core;
using SportStore.Core.Entities;

namespace SportStore.Web.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IRepository<Product> _repository;
        public NavigationMenuViewComponent(IRepository<Product> repo)
        {
            _repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(_repository.List()
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}