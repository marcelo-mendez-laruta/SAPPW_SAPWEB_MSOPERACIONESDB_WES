using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Comunes
{
    public class ConfiguracionTrama
    {
        public string transaccion { get; set; }
        public string codigo { get; set; }
        public string mensajeCorrecto { get; set; }
        public string rutaCertificado { get; set; }
    }

    public class ConfiguracionTramaBloqueo
    {
        public string transaccion { get; set; }
        public string codigo { get; set; }
        public string mensajeCorrecto { get; set; }
        public string rutaCertificado { get; set; }
        public string tipoBloqueo { get; set; }
        public string pinBlock { get; set; }
    }

    public class ConfiguracionTramaCambio
    {
        public string transaccion { get; set; }
        public string codigo { get; set; }
        public string mensajeCorrecto { get; set; }
        public string rutaCertificado { get; set; }
        public CambioData conCobro { get; set; }
        public CambioData sinCobro { get; set; }
    }

    public class CambioData {
        public string flagCobroReposicion { get; set; }
        public string pinBlock { get; set; }
        public string tipoBloqueo { get; set; }
        public string codigoBloqueo { get; set; }
        public string descripcionCobro { get; set; }

    }
}