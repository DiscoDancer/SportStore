using SportStore.Core.Entities;

namespace SportStore.Core.BusinessLogic
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}