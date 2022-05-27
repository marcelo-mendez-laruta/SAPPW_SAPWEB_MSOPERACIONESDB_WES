using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.INFOCLIENTE
{
    public class INFOCLIENTEConsultaResponse : SapResponse
    {
        public INFOCLIENTEConsultaData data { get; set; }
    }

    public class INFOCLIENTEConsultaData
    {
        public string CIC { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }

        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Nacionalidad { get; set; }
        public string Residente { get; set; }
        public string GradoInstruccion { get; set; }
        public string Telefono { get; set; }
        public string Profesion { get; set; }

        public string celular { get; set; }
        public string mail { get; set; }

        public string SituacionLaboral { get; set; }
        public string CIIU { get; set; }
        public string NegocioPropio{ get; set; }
        public string RUC { get; set; }
        public string NombreEmpresa { get; set; }
        public string NIT { get; set; }
        public string NombreComercial { get; set; }

        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string IndicadorVivienda { get; set; }

        public string TipoCuenta { get; set; }
    }
}
