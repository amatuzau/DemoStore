namespace Store.DAL.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Count { get; set; }

        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}