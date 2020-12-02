using System.Collections.Generic;

namespace Store.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
    }
}
