using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
   public class CuentaAddRequest
    {
        public string guid{ get; set; }
        public int id { get; set; }
        public bool apertura { get; set; }
        public bool original { get; set; }
        public int countFirmantes { get; set; }
        public int countProductos { get; set; }
        public int numFirmantes { get; set; }
        public string materno { get; set; }
        public string paterno { get; set; }
        public string clienteDelBanco { get; set; }
        public string codCIIU { get; set; }
        public string codSectorista { get; set; }
        public string codSucursalAgencia { get; set; }
        public string codTipoBanca { get; set; }
        public string codTipoTarjetaCredimas { get; set; }
        public string ctaAPlazoInfo { get; set; }
        public string ctaExcInfo { get; set; }
        public string direccion { get; set; }
        public string gremio { get; set; }
        public string idcN { get; set; }
        public string idcS { get; set; }
        public string idcT { get; set; }
        public string localidadDescripcion { get; set; }
        public string monto{ get; set; }
        public string nombreRazonSocial{ get; set; }
        public string nombreComercialNombreCuenta { get; set; }
        public string nroCredimas { get; set; }
        public string situacionTarjetaDescripcion { get; set; }
        public string tarjetaBancaExclusiva { get; set; }
        public string telefono { get; set; }
        public string tipoCuenta { get; set; }
        public string tipoOperacionCredimas { get; set; }
        public string sucAge { get; set; }
        public bool printOri { get; set; }
        public bool printCop { get; set; }
        public string usrCrea { get; set; }
    }
}
