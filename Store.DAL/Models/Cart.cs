using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DAL.Models
{
    public class Cart: IEntity
    {
        public int Id { get; set; }
        public Dictionary<string, int> CartItems { get; } = new Dictionary<string, int>();
    }
}
