using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SmartLink
{
    public class TarjetaBloqueoRequest : SapRequest
    {
        public TarjetaBloqueoRequestData data{get;set;}
    }
    public class TarjetaBloqueoRequestData
    {   
        public string tarjeta { get; set; }
    }
}
