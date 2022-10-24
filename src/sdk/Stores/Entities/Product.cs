using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaloCSharpSDK
{
    public class Product
    {
        public List<JObject> CateIds { get; set; } 
        public string Name { get; set; } 
        public string Desc { get; set; } 
        public string Code { get; set; } 
        public string Display { get; set; } 
        public long Price { get; set; } 
        public int Payment { get; set; } 
        public List<JObject> Photos { get; set; }  
    }
}
