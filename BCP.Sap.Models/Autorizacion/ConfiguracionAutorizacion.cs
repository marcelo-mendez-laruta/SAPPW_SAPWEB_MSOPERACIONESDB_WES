using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Autorizacion
{
    public class ConfiguracionAutorizacion: ConfiguracionWebAPI
    {
        
        public string metodo { get; set; }
        public string autorizacionUsuario { get; set; }
        public string autorizacionPassword { get; set; }
    }

    public class ConfiguracionWebAPI
    {
        public string url { get; set; }
        public string metodoClienteByIdc { get; set; }
        public string metodoClienteByCic { get; set; }
        public string metodoDatoPersonalListById { get; set; }
        public string metodoDatoLaboralListById { get; set; }
        public string metodoDatoDireccionListById { get; set; }
        public string metodoClienteInsert { get; set; }
        public string metodoDatosBasicosClienteTicket { get; set; }
        public string metodoCuentaAdd { get; set; }
        public string metodoCuentaFirmaAdd { get; set; }
        public string metodoCuentaProductoAdd { get; set; }
        public string metodoValidacionPin { get; set; }
        public string metodoActualizarTarjeta { get; set; }
        public string metodoDesafiliacionCuenta { get; set; }
        public string metodoBloqueoTarjeta { get; set; }
        public string metodoCambioApertura { get; set; }
    }
}
