using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("Service")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            ServiceIdle = new HashSet<ServiceIdle>();
            Software = new HashSet<Software>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }

        public short PaasTypeId { get; set; }

        public byte ServiceTypeId { get; set; }

        public byte? CoreCount { get; set; }

        public double? RamCount { get; set; }

        public double? HDDVolume { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public byte ServiceStateId { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostPerHour { get; set; }

        public virtual PaasType PaasType { get; set; }

        public virtual ServiceState ServiceState { get; set; }

        public virtual ServiceType ServiceType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceIdle> ServiceIdle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Software> Software { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }
    }
}
