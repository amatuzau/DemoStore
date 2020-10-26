using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.App.Models
{
    public class ProductCreateEditModel
    {
        public int? Id { get; set; }
        public string Category { set; get; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Manufactorer Manufactorer { get; set; }

        public string[] Tags { get; set; }
        public IFormFile Image { get; set; }
    }

    public class Manufactorer
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
