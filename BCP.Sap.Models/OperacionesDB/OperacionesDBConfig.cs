using BCP.Sap.Models.Autorizacion;
using BCP.Sap.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.Sap.Models.OperacionesDB
{
    public class OperacionesDBConfig : ConfiguracionBase
    {
        public string estadoGuid { get; set; }
        public ConfiguracionWebAPI configuracionSwampCore { get; set; }
        public configuracionSeguros configuracionSeguros { get; set; }
    }
    public class configuracionSeguros
    {
        public string url { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public List<Codes> Codes { get; set; }
    }
    public class Codes
    {
        public string IdProducto { get; set; }
        public string Codigo { get; set; }
    }
}
