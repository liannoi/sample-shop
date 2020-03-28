using System.ComponentModel.DataAnnotations;

namespace Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Binding
{
    public class LoginBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}