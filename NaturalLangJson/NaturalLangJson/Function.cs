using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLangJson
{
    public class Function
    {
        public string Type { get => "Function"; set => value = "Function"; }
        public string Name { get; set; }
        public List<string> Actions { get; set; }
    }
}
