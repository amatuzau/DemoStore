﻿using System.Collections.Generic;

namespace Store.DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}