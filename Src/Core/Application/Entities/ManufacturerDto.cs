using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Entities
{
    public class ManufacturerDto
    {
        public int ManufacturerId { get; set; }

        [Required] [StringLength(20)] public string ManufacturerName { get; set; }
    }
}