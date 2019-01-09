using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("CPU")]
    public partial class CPU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPU()
        {
            Server = new HashSet<Server>();
        }

        public int Id { get; set; }

        public short ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        public byte CpuSocketId { get; set; }

        public short CoreCount { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public double Frequency { get; set; }

        public virtual CpuSocket CpuSocket { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Server> Server { get; set; }
    }
}
