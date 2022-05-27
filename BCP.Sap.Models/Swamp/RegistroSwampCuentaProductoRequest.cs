using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Swamp
{
    public class RegistroSwampCuentaProductoRequest
    {
        /// <summary>
        /// Código de operacion provisto por la aplicacion.
        /// </summary>
        /// <required>true</required>
        /// <example>1234</example>
        public string operation { get; set; }
        public string usuario { get; set; }
        public RegistroSwampCuentaProductoRequestData data { get; set; }
    }
    public class RegistroSwampCuentaProductoRequestData
    {
        public string ses_guid { get; set; }
        public int cta_id { get; set; }
        public int pro_id { get; set; }
        public bool pro_nueva { get; set; }
        public string pro_clave { get; set; }
        public string pro_codmoneda { get; set; }
        public string pro_codtipoproducto { get; set; }
        public string pro_monto { get; set; }
        public string pro_numerocuenta { get; set; }
        public string pro_subcodtipoproducto { get; set; }
        public string pro_tipodpf { get; set; }
        public string pro_tipoplazo { get; set; }
    }
}
