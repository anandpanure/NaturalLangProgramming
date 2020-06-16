using NaturalLangJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonGen
{
    class Program
    {
        static void Main(string[] args)
        {
            //var solution = new Solution()
            //{
            // Name = "TestAppSolution",
            //Projects = new List<Project>()
            //{
            //    new Project()
            //    {
            //        Name = "TestApp",
            //        Classes = new List<Class>()
            //        {
            //            new Class()
            //            {
            //                Name = "HelloWorld",
            //                Function = new List<string>()
            //                {
            //                    "Main",
            //                    "HelloSpeakingWorld"
            //                },
            //                Implements = new List<string>()
            //                {
            //                    "IHelloWorld"
            //                }
            //            },
            //            new Class()
            //            {
            //                Name = "HelloWorld",
            //                Function = new List<string>()
            //                {
            //                    "Main",
            //                    "HelloSpeakingWorld"
            //                },
            //                Implements = new List<string>()
            //                {
            //                    "IHelloWorld"
            //                }
            //            }
            //        },
            //        Interfaces = new List<Interface>()
            //        {
            //            new Interface()
            //            {
            //                Name= "IHelloWorld",
            //                Functions = new List<string>()
            //                {
            //                    "HelloSpeakingWorld",
            //                }
            //            }
            //        }
            //    }
            //}

            //};

            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(solution));
            var ftp = new FunctionTemplatePopulator();
            var rv = new Dictionary<string, string>()
            {
                { ftp.Accessor, "public" },
                { ftp.Name, "Test" },
                { ftp.Paramters, "" },
                { ftp.ReturnType, "int" },
                { ftp.ClassName, "Test"},
                { ftp.SolutionPath, @"C:\NaturalLanguageProgramming\NaturalLangJson\JsonGen\"}
            };

            ftp.PopulateTemplate("Function", rv, -1);
        }
    }

    public class FunctionTemplatePopulator
    {
        public string Accessor = "Accessor";
        public string ReturnType = "ReturnType";
        public string Name = "Name";
        public string Paramters = "Paramters";
        public string ClassName = "ClassName";
        public string SolutionPath = "SolutionPath";

        public string PopulateTemplate(string templateName, Dictionary<string, string> rv, int injectAtLine)
        {
            string path = rv[SolutionPath] + rv[ClassName] + ".cs";
            string NewContent;
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {

                using (StreamReader sw = new StreamReader(fs))
                {
                    var currentContent = sw.ReadToEnd();

                    injectAtLine = currentContent.IndexOf("{", currentContent.IndexOf('{') + 1) + 1;
                    var functionDeclaration = $"{GetValue(Accessor, rv)} {GetValue(ReturnType, rv)} {GetValue(Name, rv) } ( {GetValue(Paramters, rv) } )";

                    if (currentContent.Contains(functionDeclaration))
                        return "function exists";

                    NewContent = currentContent.Substring(0, injectAtLine)
                        + Environment.NewLine
                        + functionDeclaration
                        + Environment.NewLine
                        + "{"
                        + Environment.NewLine
                        + "}"
                        + currentContent.Substring(injectAtLine, currentContent.Length - injectAtLine);
                }
            }
            File.Create(path).Close();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(NewContent);
                }
            }
            return "success";
        }

        private string GetValue(string marker, Dictionary<string, string> replacementValues)
        {
            if (replacementValues.ContainsKey(marker))
                return replacementValues[marker];

            if (marker == Accessor)
                return "public";
            else if (marker == ReturnType)
                return "void";
            else if (marker == Name)
                throw new Exception("Function needs Name");
            else return string.Empty;
        }
    }
}
