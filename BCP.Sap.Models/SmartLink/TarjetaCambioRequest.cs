using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SmartLink
{
    public class TarjetaCambioRequest:SapRequest
    {
        public TarjetaCambioRequestData data { get; set; }

        public static implicit operator TarjetaCambioRequest(TarjetaActualizacionResponse v)
        {
            throw new NotImplementedException();
        }
    }
    public class TarjetaCambioRequestData
    {
        public string tarjetaAntigua { get; set; }
        public string tarjetaNueva { get; set; }
        public string cuenta_ch_me { get; set; }
        public string cuenta_ch_mn { get; set; }
        public string cuenta_cc_me { get; set; }
        public string cuenta_cc_mn { get; set; }
    }
}
