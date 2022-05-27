using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.Monitor
{
    public class MonitorRequest
    {
        public MonitorRequestData data { get; set; }
    }

    public class MonitorRequestData
    {
        public string aplicacion { get; set; }
        public string metodo { get; set; }
        public string tipo { get; set; }
        public string mensaje { get; set; }
        public string request { get; set; }
        public string response { get; set; }
        public string excepcion { get; set; }
    }
}
