using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SmartLink
{
    public class ValidacionPinRequest:SapRequest
    {
        public ValidarPinEncriptado data { get; set; }
    }
    public class TarjetaPin
    {
        public string tarjeta { get; set; }
        
    }
    public class ValidarPinEncriptado : TarjetaPin
    {
        public string pin { get; set; }
        public string terminal { get; set; }
        public string numeroTerminal { get; set; }
    }
}
