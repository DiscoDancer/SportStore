using SportStore.Core.BusinessLogic;
using SportStore.Core.Entities;

namespace SportStore.Web.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}