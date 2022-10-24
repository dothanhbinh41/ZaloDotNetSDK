using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaloCSharpSDK
{
    public class Variation
    {
        public int Default { get; set; } 
        public double Price { get; set; } 
        public string Name { get; set; } 
        public List<string> Attributes { get; set; }  
        public string VariationId { get; set; }   
    }
}
