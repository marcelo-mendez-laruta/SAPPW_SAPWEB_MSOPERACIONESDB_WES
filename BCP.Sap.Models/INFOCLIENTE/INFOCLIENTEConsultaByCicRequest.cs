using BCP.Sap.Models.Comunes;

namespace BCP.Sap.Models.INFOCLIENTE
{
    public class INFOCLIENTEConsultaByCicRequest : SapRequest
    {
        public IdentificadorClienteCic cliente { get; set; }
    }
    public class IdentificadorClienteCic 
    {
        public string CIC { get; set; }
    }
}
