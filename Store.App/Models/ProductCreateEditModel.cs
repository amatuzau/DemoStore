using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.App.Models
{
    public class ProductCreateEditModel
    {
        public int? Id { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { set; get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    
}
