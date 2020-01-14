using System.Collections.Generic;
using SportStore.Web.Models.Dto;

namespace SportStore.Web.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}