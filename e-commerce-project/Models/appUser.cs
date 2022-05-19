using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace e_commerce_project.Models
{
    public class appUser:IdentityUser
    {
        public string Address { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
