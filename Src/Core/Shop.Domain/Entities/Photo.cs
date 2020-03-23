using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    [Table("Photo")]
    public class Photo
    {
        public int PhotoId { get; set; }

        public int GoodId { get; set; }

        [Required] [StringLength(256)] public string PhotoPath { get; set; }

        public virtual Good Good { get; set; }
    }
}