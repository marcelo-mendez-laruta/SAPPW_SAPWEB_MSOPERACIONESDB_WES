using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SmartLink
{
    public class TarjetaActualizacionRequest:SapRequest
    {
        public TarjetaActualizacionRequestData data { get; set; }
    }
    public class TarjetaActualizacionRequestData
    {
        public string tarjeta { get; set; }
        public string cuenta_ch_me { get; set; }
        public string cuenta_ch_mn { get; set; }
        public string cuenta_cc_me { get; set; }
        public string cuenta_cc_mn { get; set; }
    }
}
