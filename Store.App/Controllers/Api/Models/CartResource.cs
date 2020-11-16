namespace Store.App.Controllers.Api.Models
{
    public class CartResource
    {
        public int Id { get; set; }
        public CartItemResource[] Items { get; set; }
        public decimal Total { get; set; }
    }

    public class CartItemResource
    {
        public ProductResource Product { get; set; }
        public int Amount { get; set; }
    }
}
