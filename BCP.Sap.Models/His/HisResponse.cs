using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.His
{
    public class HisResponse : IDisposable
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public string operation { get; set; }
        public string strTramaOut { get; set; }
        public string strMensajeOut { get; set; }
        void IDisposable.Dispose() { }
    }
}