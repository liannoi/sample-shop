using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Shop.WebUI.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birhtday { get; set; }
    }
}