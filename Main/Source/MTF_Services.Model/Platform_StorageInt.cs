using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    public partial class Platform_StorageInt
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlatformId { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte InterfaceId { get; set; }

        public byte SlotCount { get; set; }

        public virtual Platform Platform { get; set; }

        public virtual StrorageInterface StrorageInterface { get; set; }
    }
}
