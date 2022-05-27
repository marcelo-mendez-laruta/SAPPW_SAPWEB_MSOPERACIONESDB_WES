using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Swamp
{
    public class RegistroSwampCuentaFirmaRequest
    {
        /// <summary>
        /// Código de operacion provisto por la aplicacion.
        /// </summary>
        /// <required>true</required>
        /// <example>1234</example>
        public string operation { get; set; }
        public string usuario { get; set; }
        public RegistroSwampCuentaFirmaRequestData data { get; set; }
        public IdentificadorClienteNatural cliente { get; set; }
    }
    public class RegistroSwampCuentaFirmaRequestData
    {
        public string ses_guid { get; set; }
        public int cta_id { get; set; }
        public int fir_id { get; set; }
        public bool fir_actualizadatos { get; set; }
        public bool fir_clientenuevo { get; set; }
        public string fir_apmaterno { get; set; }
        public string fir_appaterno { get; set; }
        public string fir_estadocivil { get; set; }
        public string fir_fechanac { get; set; }
        public string fir_nombres_razsocial { get; set; }
        public string fir_numerocredimas { get; set; }
    }
}
