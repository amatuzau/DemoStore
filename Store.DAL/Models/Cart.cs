using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        [NotMapped]
        public decimal Total { get; set; }
    }
}
