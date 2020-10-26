using Store.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class StoreViewModel
    {
        public Category[] Categories { get; set; }
        public Product[] Products { get; set; }
    }
}
