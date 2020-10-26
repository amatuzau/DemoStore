namespace Store.DAL.Models
{
    public class Product: IEntity
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

    }
}
