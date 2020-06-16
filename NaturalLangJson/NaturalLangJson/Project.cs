using System.Collections.Generic;

namespace NaturalLangJson
{
    public class Project
    {
        public string Type { get => "Project"; set => value = "Project"; }
        public string Name { get; set; }
        public List<Class> Classes { get; set; }
        public List<Interface> Interfaces { get; set; }
    }
}