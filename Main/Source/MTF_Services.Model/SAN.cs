using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("SAN")]
    public partial class SAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAN()
        {
            SAN_MaintenanceShedule = new HashSet<SAN_MaintenanceShedule>();
            SAN_Storage = new HashSet<SAN_Storage>();
            SAN_StorageInt = new HashSet<SAN_StorageInt>();
            PaasType = new HashSet<PaasType>();
        }

        public int Id { get; set; }

        public short ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Column(TypeName = "money")]
        public decimal? AnnualMaintenance { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Active { get; set; }

        public bool OnMaintenance { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAN_MaintenanceShedule> SAN_MaintenanceShedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAN_Storage> SAN_Storage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAN_StorageInt> SAN_StorageInt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaasType> PaasType { get; set; }
    }
}
