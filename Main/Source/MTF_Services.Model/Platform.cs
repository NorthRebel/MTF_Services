using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("Platform")]
    public partial class Platform
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Platform()
        {
            Platform_StorageInt = new HashSet<Platform_StorageInt>();
            Server = new HashSet<Server>();
        }

        public int Id { get; set; }

        public short ManufacturerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        public byte CpuSocketId { get; set; }

        public byte CPUCount { get; set; }

        public byte RamTypeId { get; set; }

        public int RamVolumeMax { get; set; }

        public byte RamSocketCount { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual CpuSocket CpuSocket { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual RamType RamType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Platform_StorageInt> Platform_StorageInt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Server> Server { get; set; }
    }
}
