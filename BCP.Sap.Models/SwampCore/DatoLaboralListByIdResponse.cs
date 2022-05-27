using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class DatoLaboralListByIdResponse:SwampCoreResponse
    {
        public int clienteId { get; set; }
        public string situacionLaboralDescripcion { get; set; }
        public string situacionLaboralAbreviacion { get; set; }
        public string nombreEmpresa { get; set; }
        public string nit { get; set; }
        public string cargoLaboralDescricpin { get; set; }
        public string cargoLaboralAbreviacion { get; set; }
        public string actividadCodigo { get; set; }
        public string actividadDescripcion { get; set; }
        public string ciiuCodigo { get; set; }
        public string ciiuDescripcion { get; set; }
        //public int salario { get; set; }
        public string codigotelefonoDescripcion { get; set; }
        public string codigotelefonoAbreviacion { get; set; }
        public string telefono { get; set; }
        public string anexoTelefono { get; set; }
        public string celular { get; set; }
        public string correoElectronico { get; set; }
        public bool cargoPublico { get; set; }
        public string cargo { get; set; }
        public string periodo { get; set; }
        public bool negocioPropio { get; set; }
        public string fechaIngreso { get; set; }
        public bool funcionarioPublico { get; set; }
    }
}
