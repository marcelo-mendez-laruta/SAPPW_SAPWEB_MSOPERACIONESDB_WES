using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.His
{
    public class HisRequest
    {
        public string operacion { get; set; }
        public string ip { get; set; }
        public string puerto { get; set; }
        public string puerto_respaldo { get; set; }
        public string teti { get; set; }
        public string lu { get; set; }
        public int timeOut { get; set; }
        public string trama { get; set; }
    }
}
