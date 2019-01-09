using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("Server")]
    public partial class Server
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Server()
        {
            Server_MaintenanceShedule = new HashSet<Server_MaintenanceShedule>();
            Server_RAM = new HashSet<Server_RAM>();
            Server_Storage = new HashSet<Server_Storage>();
            PaasType = new HashSet<PaasType>();
        }

        public int Id { get; set; }

        public int PlatformId { get; set; }

        public int CPU_Id { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [Column(TypeName = "money")]
        public decimal? AnnualMaintenance { get; set; }

        public bool Active { get; set; }

        public bool OnMaintenance { get; set; }

        public virtual CPU CPU { get; set; }
        public virtual Platform Platform { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Server_MaintenanceShedule> Server_MaintenanceShedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Server_RAM> Server_RAM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Server_Storage> Server_Storage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaasType> PaasType { get; set; }
    }
}
