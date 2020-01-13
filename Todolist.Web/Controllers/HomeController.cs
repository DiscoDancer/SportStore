using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todolist.Core;
using Todolist.Core.Entities;
using Todolist.Web.Models;

namespace Todolist.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            int recordsAdded = DatabasePopulator.PopulateDatabase(_repository);
            var items = _repository.List<ToDoItem>().ToArray();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
