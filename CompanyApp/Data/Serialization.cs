using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Data
{
    public class Serialization
    {
        public static string Serialize(object Object)
        {
            return JsonConvert.SerializeObject(Object,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
        }

        public static string Serialize(IEnumerable<object> Objects) 
        {
            return JsonConvert.SerializeObject(Objects, 
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        public static object DeSerialize(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}
