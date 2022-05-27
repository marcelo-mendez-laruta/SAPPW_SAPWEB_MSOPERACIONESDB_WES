using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class CambioAperturaRequest
    {
        public string tarjeta { get; set; }
        public string cuenta { get; set; }
        public string cliente { get; set; }
    }
}
