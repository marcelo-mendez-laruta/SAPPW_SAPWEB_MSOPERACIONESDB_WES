using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class CuentaFirmaAddRequest
    {
        public string guid { get; set; }
        public int ctaId { get; set; }
        public int firId { get; set; }
        public bool actualizaDatos { get; set; }
        public bool clienteNuevo { get; set; }
        public string materno { get; set; }
        public string paterno { get; set; }
        public string estadoCivil { get; set; }
        public string fechNac { get; set; }
        public string idcN { get; set; }
        public string idcS { get; set; }
        public string idcT { get; set; }
        public string nombresRazSocial { get; set; }
        public string nroCredimas { get; set; }
        public string usrCreacion { get; set; }
    }
}
