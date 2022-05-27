using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Autorizacion
{
    public class AutorizacionResponse
    {
        public string state { get; set; }
        public string message { get; set; }
        public AutorizacionData data { get; set; }
    }

    public class AutorizacionData
    {
        public int id { get; set; }
        public string username { get; set; }
        public string publicToken { get; set; }
        public string status { get; set; }
        public string date { get; set; }
    }
}