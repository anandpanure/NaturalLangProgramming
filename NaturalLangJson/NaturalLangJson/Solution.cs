using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLangJson
{
    public class Solution
    {
        public string Type { get => "Solution"; set => value = "Solution"; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}
