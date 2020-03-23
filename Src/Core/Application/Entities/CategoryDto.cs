using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Entities
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        [Required] [StringLength(20)] public string CategoryName { get; set; }
    }
}