using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Domain.Entities
{
    [Table("Good")]
    public class Good
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Good()
        {
            Photos = new HashSet<Photo>();
            SalePos = new HashSet<SalePos>();
        }

        [Key] public int GoodId { get; set; }

        [Required] [StringLength(100)] public string GoodName { get; set; }

        public int? ManufacturerId { get; set; }

        public int? CategoryId { get; set; }

        [Column(TypeName = "money")] public decimal Price { get; set; }

        [Column(TypeName = "numeric")] public decimal GoodCount { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePos> SalePos { get; set; }
    }
}