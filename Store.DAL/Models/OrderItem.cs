namespace Store.DAL.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
