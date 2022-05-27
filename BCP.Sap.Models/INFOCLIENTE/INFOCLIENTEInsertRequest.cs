using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.INFOCLIENTE
{
    public class INFOCLIENTEInsertRequest : SapRequest
    {
        public string usuario { get; set; }
        public IdentificadorClienteNatural cliente { get; set; }
        public INFOCLIENTEInsertData data { get; set; }
    }

    public class INFOCLIENTEInsertData
    {
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Nacionalidad { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public string GradoInstruccion { get; set; }
        public string Profesion { get; set; }
        public string SituacionLaboral { get; set; }
        public string TipoVivienda { get; set; }
        public string Residente { get; set; }
        public string NombreEmpresa { get; set; }
        public string NIT { get; set; }
        public string CIIU { get; set; }
        public string MagnitudEmpresa { get; set; }
        public string CIC { get; set; }
        public string Domicilio { get; set; }
        public string negocioPropio { get; set; }

        public string Mail { get; set; }
        public string Celular { get; set; }
        public string ciiu2 { get; set; }

        public string strCalle { get; set; }
        public string strNumeroDomicilio { get; set; }
        public string strManzana { get; set; }
        public string strLote { get; set; }
        public string strDepartamento { get; set; }
        public string strDepPiso { get; set; }
        public string strUrbanizacionTipo { get; set; }  //los tipo son los nombres de los combobox      
        public string strUrbanizacion { get; set; }
        public string strSectorTipo { get; set; }
        public string strSector { get; set; }
        /* public string CodigoSBS { get; set; }    
         public string Correspondecia { get; set; }
         public string FechaUltActualizacion { get; set; }        
         public string FinSocial { get; set; }
         public string FechaConstitucion { get; set; }
         public string Categoria { get; set; }
         public string NaturalezaJuridica { get; set; }
         public string RazonSocial { get; set; }
         public string Sucursal { get; set; }
         public string SucursalAgencia { get; set; }*/

    }
}
