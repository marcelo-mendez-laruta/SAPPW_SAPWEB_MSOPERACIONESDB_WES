using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SmartLink
{
    public class CuentaRegistroRequest:SapRequest
    {
        public CuentaRegistroRequestData data { get; set; }
    }
    public class CuentaRegistroRequestData
    {
        public string nombreCliente { get; set; }
        public string cuenta { get; set; }
        public string tarjeta { get; set; }
    }
}
