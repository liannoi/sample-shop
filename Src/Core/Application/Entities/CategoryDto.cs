using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Entities
{
    public class CategoryDto
    {
        [Key] public int CategoryId { get; set; }

        [Required] [StringLength(20)] public string CategoryName { get; set; }
    }
}