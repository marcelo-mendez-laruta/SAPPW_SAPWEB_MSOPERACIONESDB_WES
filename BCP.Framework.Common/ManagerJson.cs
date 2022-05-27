using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Framework.Common
{
    public class ManagerJson
    {
        public static T Deserialize<T>(string _object)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(_object);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodBase.GetCurrentMethod() + " - A problem occurred: ", ex);
            }
        }

        public static string Serialize(object _object)
        {
            try
            {
                return JsonConvert.SerializeObject(_object);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodBase.GetCurrentMethod() + " - A problem occurred: ", ex);
            }
        }
    }
}