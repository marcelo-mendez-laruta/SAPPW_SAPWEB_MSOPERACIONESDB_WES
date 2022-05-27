using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Swamp
{
    public class RegistroSwampCuentaRequest:SapRequest
    {
        public IdentificadorClienteNatural cliente { get; set; }
        public RegistroSwampCuentaRequestData data { get; set; }
        public Usuario usuario { get; set; }
    }
    public class RegistroSwampCuentaRequestData
    {
        public string ses_guid { get; set; }
        public int cta_id { get; set; }
        public bool cta_apertura { get; set; }
        public bool cta_original { get; set; }
        public int cta_countfirmantes { get; set; }
        public int cta_countproductos { get; set; }
        public int cta_numfirmantes { get; set; }
        public string cta_apmaterno { get; set; }
        public string cta_appaterno { get; set; }
        public string cta_clientedelbanco { get; set; }
        public string cta_codciiu { get; set; }
        public string cta_codigosectorista { get; set; }
        public string cta_codSucursalAgencia { get; set; }
        public string cta_codtipobanca { get; set; }
        public string cta_codtipotarjetacredimas { get; set; }
        public string cta_ctaaplazoinfo { get; set; }
        public string cta_ctaexcinfo { get; set; }
        public string cta_direccion { get; set; }
        public string cta_gremio { get; set; }
        public string cta_localidaddescripcion { get; set; }
        public string cta_monto { get; set; }
        public string cta_nombres_razsocial { get; set; }
        public string cta_nomcomerc_nomcuenta { get; set; }
        public string cta_numerocredimas { get; set; }
        public string cta_situaciontarjetadescripcion { get; set; }
        public string cta_tarjetabancaexclusiva { get; set; }
        public string cta_telefono { get; set; }
        public string cta_tipocuenta { get; set; }
        public string cta_tipooperacioncredimas { get; set; }
    }
}
