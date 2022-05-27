using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Comunes
{
    public class SapResponse
    {
        public string state { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public string operation { get; set; }

        public List<string> errors { get; set; }
    }
}
