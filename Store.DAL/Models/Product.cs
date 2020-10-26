using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DAL.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { set; get; }
        public string Name { get; set; }
        public string Image { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}