using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class DatosBasicosClienteTicketResponse:SwampCoreResponse
    {
        public string idc { get; set; }
        public string extension { get; set; }
        public string tipo { get; set; }
        public string complemento { get; set; }
        public string cic { get; set; }
        public string aplicativo { get; set; }
        public string ticket { get; set; }
    }
}
