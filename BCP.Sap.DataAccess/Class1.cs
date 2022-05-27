using System;
using System.ServiceModel;
using srvContrato;

namespace BCP.Sap.DataAccess
{
    public class Class1
    {
        CREDENCIALES_AUTH _c;
        srvContrato.ServicioAfiliacionAuthClient contratos;
        public Class1() {
            _c = new CREDENCIALES_AUTH
            {
                IP = "172.31.12.24",
                USUARIO="ESTACION",
                PASSWORD= "ESTACION"
            };
            EndpointAddress addres = new EndpointAddress("http://devcrs02:3007/CRSclic.AfiliacionIBK.Core/ServicioAfiliacionAuth.svc");
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            contratos = new srvContrato.ServicioAfiliacionAuthClient(binding, addres);
            
        }
        public string certificado()
        {
            var x = contratos.GetFileCertificado("N000000008462099XX199931GVRI", 1065, "SEGMUL", _c);
            contratos.Close();
            if (x.CODIGO_ERROR.Equals("200"))
            {
                return System.Convert.ToBase64String(x.IMAGEN);
            }
            else
            {
                return x.MENSAJE_ERROR;
            }
        }
    }
}
