using Microsoft.AspNet.Identity.EntityFramework;

namespace Shop.Legacy.WebUI.System.Identity.Core.Models
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