using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class  DatoPersonalListByIdResponse:SwampCoreResponse
    {
        public int clienteId { get; set; }
        public string fechaNacimiento { get; set; }
        public string sexoDescripcion { get; set; }
        public string sexoAbreviacion { get; set; }
        public string estadoCivilDescripcion { get; set; }
        public string estadoCivilAbreviacion { get; set; }
        public string nacionalidadDescripcion { get; set; }
        public string nacionalidadAbreviacion { get; set; }
        public bool residente { get; set; }
        public string profesionDescripcion { get; set; }
        public string gradoInstruccionDescripcion { get; set; }
        public string gradoInstruccionAbreviacion { get; set; }
        public string codigoTelefonoDescripcion { get; set; }
        public string codigoTelefonoAbreviacion { get; set; }
        public string telefono { get; set; }
        public string anexo { get; set; }
        public string codigoCelularDescripcion { get; set; }
        public string codigoCelularAbreviacion { get; set; }
        public string celular { get; set; }
        public string correoElectronico { get; set; }
        public string conyIdc { get; set; }
        public string conyNombre { get; set; }
        public string conyPaterno { get; set; }
        public string conyMaterno { get; set; }
        public string conyNacionalidad { get; set; }
        public string conyResidente { get; set; }
        public string banco1 { get; set; }
        public string producto1 { get; set; }
        public string banco2 { get; set; }
        public string producto2 { get; set; }
        public string banco3 { get; set; }
        public string producto3 { get; set; }
        public string refPer1Nombre { get; set; }
        public string refPer1Direccion { get; set; }
        public string refPer1Telefono { get; set; }
        public string refPer2Nombre { get; set; }
        public string refPer2Direccion { get; set; }
        public string refPer2Telefono { get; set; }
        public string refPer3Nombre { get; set; }
        public string refPer3Direccion { get; set; }
        public string refPer3Telefono { get; set; }
        public string refCom1Nombre { get; set; }
        public string refCom1Direccion { get; set; }
        public string refCom1telefono { get; set; }
        public string refCom2Nombre { get; set; }
        public string refCom2Direccion { get; set; }
        public string refCom2telefono { get; set; }
        public string refCom3Nombre { get; set; }
        public string refCom3Direccion { get; set; }
        public string refCom3telefono { get; set; }
        public string complemento { get; set; }
        public string extension { get; set; }
        public string conyComplemto { get; set; }
        public string desmaterializacion { get; set; }
    }
}
