using BCP.Sap.Models.Autorizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Configuracion
{
    public class ConfiguracionBase
    {
        public ConfiguracionAplicacion configuracionAplicacion { get; set; }
        public ConfiguracionLogs configuracionLog { get; set; }
        public ConfiguracionAutorizacion configuracionAutorizacion { get; set; }
        public List<ConexionBaseDeDatos> bases { get; set; }
        public ConfiguracionSocket socket { get; set; }
    }
}
