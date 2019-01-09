using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    [Table("ServiceIdle")]
    public partial class ServiceIdle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceIdle()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ServiceId { get; set; }

        public DateTime BeginPeriod { get; set; }

        public DateTime EndPeriod { get; set; }

        public byte IdleTypeId { get; set; }

        public byte IdleReasonId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }

        public virtual IdleReason IdleReason { get; set; }

        public virtual IdleType IdleType { get; set; }

        public virtual Service Service { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }
    }
}
