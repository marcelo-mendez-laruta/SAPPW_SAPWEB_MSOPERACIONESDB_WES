using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Comunes
{
    public class SapRequest
    {
        /// <summary>
        /// Código de operacion provisto por la aplicacion.
        /// </summary>
        /// <required>true</required>
        /// <example>1234</example>
        public string operation { get; set; }

        
    }
}
