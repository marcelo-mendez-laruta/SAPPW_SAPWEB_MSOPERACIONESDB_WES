using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Comunes
{
    public class Usuario
    {
        /// <summary>
        /// Matricula de un usuario de extra con acceso a CONSIST.
        /// </summary>
        /// <required>true</required>
        /// <example>S38994</example>
        public string usuarioExtra { get; set; }
        /// <summary>
        /// Código de la sucursal a la cual pertenece el usuario de EXTRA.
        /// </summary>
        /// <required>true</required>
        /// <example>201</example>
        public string sucursal { get; set; }
        /// <summary>
        /// Código de la agencia a la cual pertenece el usuario de EXTRA.
        /// </summary>
        /// <required>true</required>
        /// <example>204</example>
        public string agencia { get; set; }      

        /// <summary>
        /// Guid de sesión de SWAMP.
        /// </summary>
        /// <required>true</required>
        /// <example>1b4d8363-4d0d-470a-998d-380ec1ac0efc</example>
        public string guid { get; set; }        
    }
}
