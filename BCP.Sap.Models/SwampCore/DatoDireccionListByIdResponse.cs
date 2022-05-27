using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class DatoDireccionListByIdResponse:SwampCoreResponse
    {
        public List<DatoDireccionListByIdResponseData> lstDirecciones { get; set; }
    }
    public class DatoDireccionListByIdResponseData
    {
        public int clienteId { get; set; }
        public int idDireccion { get; set; }
        public string direccion { get; set; }
        public string tipoDireccionDescripcion{ get; set; }
        public string tipoDireccionAbreviacion { get; set; }
        public string tipoViviendaDescripcion { get; set; }
        public string tipoViviendaAbreviacion { get; set; }
        public string tipoDefDireccionDescripcion{ get; set; }
        public string tipoDefDireccionAbreviacion { get; set; }
        public string numeroDireccion { get; set; }
        public string manzanaDireccion { get; set; }
        public string loteDireccion { get; set; }
        public string tipoDepartamentoDescripcion { get; set; }
        public string tipoDepartamentoAbreviacion { get; set; }
        public string numeroDepartamento { get; set; }
        public string urbanizacionDescripcion { get; set; }
        public string urbanizacionAbreviacion { get; set; }
        public string nombreUrbanizacion { get; set; }
        public string sectUrbanizacionDescripcion { get; set; }
        public string sectUrbanizacionAbreviacion { get; set; }
        public string nombreSectorUrbanizacion { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
    }
}
