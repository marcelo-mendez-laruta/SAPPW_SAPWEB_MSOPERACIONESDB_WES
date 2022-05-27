using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Comunes
{
    public class IdentificadorClienteJuridico
    {
        /// <summary>
        /// Parte numérica del IDC del cliente.
        /// </summary>
        /// <required>true</required>
        /// <example>01020435</example>
        public string idcNumero { get; set; }
        /// <summary>
        /// Tipo de documento del IDC del cliente.
        /// </summary>
        /// <example>R</example>
        public string idcTipo { get; set; }
        /// <summary>
        /// Extensión del documento IDC del cliente.
        /// </summary>
        /// <example>022</example>
        public string idcExtension { get; set; }
    }
}
