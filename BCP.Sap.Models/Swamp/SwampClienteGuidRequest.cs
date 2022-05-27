using BCP.Sap.Models.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Models.Swamp
{
    public class SwampClienteGuidRequest:SapRequest
    {
        /// <summary>
        /// Guid de Swamp.
        /// </summary>
        /// <required>true</required>
        /// <example>00007be9-beaf-4229-abeb-e0d5fbc34cf8</example>
        public string guid { get; set; }
    }
}
