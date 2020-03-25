using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Application.Entities
{
    public class GoodDto
    {
        [Key] public int GoodId { get; set; }

        [Required] [StringLength(100)] public string GoodName { get; set; }

        public int? ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public string CategoryName { get; set; }

        public int? CategoryId { get; set; }

        [Column(TypeName = "money")] public decimal Price { get; set; }

        [Column(TypeName = "numeric")] public decimal GoodCount { get; set; }
    }
}