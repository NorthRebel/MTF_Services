using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("RAM")]
    public partial class RAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RAM()
        {
            Server_RAM = new HashSet<Server_RAM>();
        }

        public int Id { get; set; }

        public short ManufacturerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        public byte RamTypeId { get; set; }

        public int Volume { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual RamType RamType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Server_RAM> Server_RAM { get; set; }
    }
}
