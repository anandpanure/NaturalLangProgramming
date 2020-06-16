using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLangJson
{
    public class Class
    {
        public string Type { get => "Class"; set => value = "Class"; }
        public string Name { get; set; }
        public List<string> Implements { get; set; }
        public List<string> Function { get; set; }
    }
}
