using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Domain.Entities
{
    [Table("Category")]
    public class Category
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Goods = new HashSet<Good>();
        }

        public int CategoryId { get; set; }

        [Required] [StringLength(20)] public string CategoryName { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Good> Goods { get; set; }
    }
}