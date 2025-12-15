using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Models
{
    public class Business
    {
        public string id { get; set; }
        public string BusinssName { get; set; }
        public Products[] Products { get; set; }
        public Branches[] Branches { get; set; }


        public static List<Business> ConvertStringIntoList(string businessAsList)
        {
            if (string.IsNullOrEmpty(businessAsList)) return new List<Business>();
            return System.Text.Json.JsonSerializer.Deserialize<List<Business>>(businessAsList);
        }
    }
}
