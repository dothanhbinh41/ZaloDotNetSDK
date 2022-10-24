using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaloCSharpSDK
{
    public class RelatedMedia
    { 
        public string Id { get; private set; }
        public RelatedMedia(string id)
        {
            Id = id;
        } 
    }
}
