using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Autorizacion
{
    public class AutorizacionRequest
    {
        public string date { get; set; }
        public string channel { get; set; }
        public string publicToken { get; set; }
        public string appUserId { get; set; }
    }
}