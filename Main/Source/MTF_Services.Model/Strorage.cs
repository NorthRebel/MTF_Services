using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("Strorage")]
    public partial class Strorage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Strorage()
        {
            SAN_Storage = new HashSet<SAN_Storage>();
            Server_Storage = new HashSet<Server_Storage>();
        }

        public int Id { get; set; }

        public short ManufacturerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        public byte StrorageInterfaceId { get; set; }

        public int Volume { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAN_Storage> SAN_Storage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Server_Storage> Server_Storage { get; set; }

        public virtual StrorageInterface StrorageInterface { get; set; }
    }
}
