using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.SwampCore
{
    public class ClienteInsertRequest
    {
        public string idc { get; set; }
        public string extension  { get; set; }
        public string tipo  { get; set; }
        public string paterno  { get; set; }
        public string materno  { get; set; }
        public string nombres  { get; set; }
        public string fechaNacimiento  { get; set; }
        public string sexo  { get; set; }
        public string estadoCivil  { get; set; }
        public string nacionalidad  { get; set; }
        public string calle  { get; set; }
        public string direccion  { get; set; }
        public string numero  { get; set; }
        public string manzana  { get; set; }
        public string lote  { get; set; }
        public string departamento  { get; set; }
        public string departamentoPiso  { get; set; }
        public string urbanizacionTipo  { get; set; }
        public string urbanizacion  { get; set; }
        public string sectorTipo  { get; set; }
        public string sector  { get; set; }
        public string localidad  { get; set; }
        public string telefono  { get; set; }
        public string instruccion  { get; set; }
        public string profesion  { get; set; }
        public string situacionLaboral  { get; set; }
        public string condVivienda  { get; set; }
        public string residente  { get; set; }
        public string negocioPropio  { get; set; }
        public string empresa { get; set; }
        public string nit  { get; set; }
        public string email  { get; set; }
        public string celular  { get; set; }
        public string ciiu  { get; set; }
        public string magnitud  { get; set; }
        public string ciiu2  { get; set; }
        public string cic { get; set; }
        public string usuario  { get; set; }
        public string canal  { get; set; }
    }
}
