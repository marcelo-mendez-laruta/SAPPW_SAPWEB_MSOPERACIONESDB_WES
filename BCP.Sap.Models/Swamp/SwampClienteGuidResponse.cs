using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Swamp
{
    public class SwampClienteGuidResponse:SapResponse
    {
        public SwampClienteGuidResponseData data { get; set; }
    }
    public class SwampClienteGuidResponseData : IdentificadorClienteJuridico//IdentificadorClienteNatural
    {
        public string guid { get; set; }
    }
}
