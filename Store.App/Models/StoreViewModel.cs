using Store.DAL.Models;

namespace Store.App.Models
{
    public class StoreViewModel
    {
        public Category[] Categories { get; set; }
        public Product[] Products { get; set; }
    }
}