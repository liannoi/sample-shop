using Microsoft.AspNet.Identity.EntityFramework;

namespace Shop.WebUI.Identity.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
        }

        public AppRole(string name) : base(name)
        {
            Name = name;
        }
    }
}