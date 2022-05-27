using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Framework.Common
{
    public static class ManagerValidation
    {
        public static string GetDefaultValue(string strText, string strDefault)
        {
            string response = string.Empty;
            try
            {
                response = (string.IsNullOrEmpty(strText)) ? strDefault.ToUpper() : strText.ToUpper().Trim();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        public static string intSTR(string valor)
        {
            if (valor.Length > 1)
            {
                while (valor[0] == '0')
                    valor = valor.Substring(1);
            }
            return valor;
        }

        public static string formatoVivienda(string info)
        {
            string host = "NODIO";
            try
            {
                switch (info)
                {
                    case "ALQ.":
                        host = "ALQUI";
                        break;
                    case "ANTI":
                        host = "ANTIC";
                        break;
                    case "FAMIL":
                        host = "FAMIL";
                        break;
                    case "OTRAS":
                        host = "OTRAS";
                        break;
                    case "PROP.":
                        host = "PRPIA";
                        break;
                    default:
                        host = "NODIO";
                        break;
                }
            }
            catch (Exception)
            {

            }
            return host;
        }

        public static string formatoTipoCalle(string tipoCalle)
        {
            string response = string.Empty;
            try
            {
                switch (tipoCalle)
                {
                    case "CA": 
                    case"AV":
                        response = tipoCalle + ".";
                    break;
                    case "CJON":
                        response = "CJ.";
                        break;
                    case "PZA":
                        response = "PZ.";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}
