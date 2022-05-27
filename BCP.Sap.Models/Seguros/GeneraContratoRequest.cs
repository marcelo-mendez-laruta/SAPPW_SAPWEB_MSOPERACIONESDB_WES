using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Seguros
{
    public class GeneraContratoRequest:SapRequest
    {
        public GeneraContratoRequestData data { get; set; }
    }
    public class GeneraContratoRequestData
    {
        public long IdAfiliacion { get; set; }
        public string IdPersona { get; set; }
        public string CodigoProducto { get; set; }
        public string IpOrigen { get; set; }
    }
}
