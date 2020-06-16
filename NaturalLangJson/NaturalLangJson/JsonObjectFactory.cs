using System.Collections.Generic;

namespace NaturalLangJson
{
    public class JsonObjectFactory
    {
        public Solution GetSolution()
        {
              return  new Solution()
                {
                    Name = "Dummy",
                    Projects = new List<Project>()
                    {
                        new Project()
                        {
                            Name = "Dummy",
                            Classes = new List<Class>()
                            {
                            },
                            Interfaces = new List<Interface>()
                            {

                            }
                        }
                     }
                };
        }
    }
}
