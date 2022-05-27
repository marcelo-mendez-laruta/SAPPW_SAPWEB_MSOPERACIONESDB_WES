using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class CuentaProductoAddRequest
    {
        public string guid { get; set; }
        public int id { get; set; }
        public int prodId { get; set; }
        public bool nueva { get; set; }
        public string clave { get; set; }
        public string codMoneda { get; set; }
        public string codTipoProducto { get; set; }
        public string monto { get; set; }
        public string numeroCuenta { get; set; }
        public string subCodTipoPorProducto { get; set; }
        public string tipoDPF { get; set; }
        public string tipoPlazo { get; set; }
        public string usrCrea { get; set; }
    }
}
