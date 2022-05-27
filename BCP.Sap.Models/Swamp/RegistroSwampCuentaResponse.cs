using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Swamp
{
    public class RegistroSwampCuentaResponse:SapResponse
    {
        public RegistroSwampCuentaResponseData data { get; set; }
    }
    public class RegistroSwampCuentaResponseData
    {
        public int idCuenta { get; set; }
    }
}
