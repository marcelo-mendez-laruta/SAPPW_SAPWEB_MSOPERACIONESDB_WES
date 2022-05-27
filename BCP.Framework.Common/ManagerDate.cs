using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Framework.Common
{
    public class ManagerDate
    {
        public static string ChangeFormatDate(string strDateIN, string strFormatIN, string strFormatOUT)
        {
            string resultado = string.Empty;
            try
            {
                DateTime dtFecha;
                DateTime.TryParseExact(strDateIN, strFormatIN, null, DateTimeStyles.None, out dtFecha);
                resultado = dtFecha.ToString(strFormatOUT);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public static string GetDateCustomFormat(string strFormatOUT)
        {
            string resultado = string.Empty;
            try
            {
                DateTime dtFecha = DateTime.Now;
                resultado = dtFecha.ToString(strFormatOUT, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
    }
}