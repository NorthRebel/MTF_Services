using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTF_Services.Model.Views
{
    public class ServerPaasInfo
    {
        public int Id { get; set; }
        public string Platform { get; set; }
        public string CPU { get; set; }
        public int AvalibleCoreCount { get; set; }
        public int UsedCoreCount { get; set; }
        public double AvalibleRAMVolume { get; set; }
        public double UsedRAMVolume { get; set; }
        public double AvalibleStorageVolume { get; set; }
        public double UsedStorageVolume { get; set; }
    }
}
