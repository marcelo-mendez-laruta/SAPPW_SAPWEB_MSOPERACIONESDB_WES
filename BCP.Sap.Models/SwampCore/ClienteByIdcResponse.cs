using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class ClienteByIdcResponse: SwampCoreResponse
    {
        public string cic { get; set; }
        public string complemento { get; set; }
        public string nombres { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public int clienteId { get; set; }
    }
}
