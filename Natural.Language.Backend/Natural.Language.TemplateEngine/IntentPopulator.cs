using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace Natural.Language.TemplateEngine
{
    public class IntentPopulator : TemplatePopulator, IPopulateTemplate
    {
        public List<string> RequiredKeys()
        {
            return new List<string>() 
            {
                VariableName
            };
        }

        public string PopulateTemplate(string templateName, Dictionary<string, string> rv, int injectAtLine)
        {
            try
            {
                StringBuilder functionTemplate = LoadTemplate(templateName);

                string path = Path.Combine(ProjectPath, rv[ProjectName], "Program.cs");
                string NewContent;
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (StreamReader sw = new StreamReader(fs))
                    {
                        var currentContent = sw.ReadToEnd();

                        injectAtLine = currentContent.IndexOf("//Insertion Point//") -1;

                        string code = string.Empty;

                        code = Populate(functionTemplate, rv).ToString();
                        if (!currentContent.Contains(rv[VariableName]))
                        {
                            code = "var " + code;
                        }

                        NewContent = currentContent.Insert(injectAtLine, code);
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
            catch (Exception e)
            {
                return e.Message;
            }
        }



    }
}
