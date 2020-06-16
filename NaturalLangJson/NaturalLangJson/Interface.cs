using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLangJson
{
    public class Interface
    {
        public string Type { get => "Interface"; set => value = "Interface"; }
        public string Name { get; set; }
        public List<string> Functions { get; set; }
    }
}
