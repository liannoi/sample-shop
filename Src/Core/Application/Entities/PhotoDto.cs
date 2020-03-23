using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Entities
{
    public class PhotoDto
    {
        public int PhotoId { get; set; }

        public int GoodId { get; set; }

        [Required] [StringLength(256)] public string PhotoPath { get; set; }
    }
}