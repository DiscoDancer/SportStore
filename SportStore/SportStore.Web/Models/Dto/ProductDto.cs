using Microsoft.AspNetCore.Routing.Constraints;

namespace SportStore.Web.Models.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int Id { get; set; }
    }
}