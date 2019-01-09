using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTF_Services.Model
{
    public partial class SAN_Storage
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SAN_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StorageId { get; set; }

        public short? Count { get; set; }

        public virtual SAN SAN { get; set; }

        public virtual Strorage Strorage { get; set; }
    }
}
