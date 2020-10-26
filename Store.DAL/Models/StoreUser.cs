using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DAL.Models
{
    public class StoreUser: IdentityUser
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
