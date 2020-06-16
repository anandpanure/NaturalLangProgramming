using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Natural.Language.TemplateEngine
{
    //requires classname solutionpath and function name to operate
    public class ClassTemplatePopulator : TemplatePopulator, IPopulateTemplate
    {
        private bool CheckProjectExistance(string projName)
        {
            var path = Path.Combine(ProjectPath, projName, $"{projName}.csproj");

            return File.Exists(path);
        }

        public string PopulateTemplate(string templateName, Dictionary<string, string> rv, int injectAtLine)
        {
            try
            {
                StringBuilder classTemplate = LoadTemplate(templateName);
                PopulateDefaults(rv);

                string path = Path.Combine(ProjectPath,rv[ProjectName],$"{rv[ClassName]}.cs");
                if (File.Exists(path))
                    return "Class Exists";

                if (!CheckProjectExistance(rv[@"@PROJECT_NAME@"]))
                    return "Project File Doesn't exists";

                File.Create(path).Close();
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(Populate(classTemplate, rv).ToString());
                    }
                }

                // Modify the Project File.
                var projModification = ModifyProjFile(rv[@"@PROJECT_NAME@"],rv[@"@CLASS_NAME@"]);
                if (!projModification)
                    return "Project Modification Failed";

                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        protected void PopulateDefaults(Dictionary<string, string> replacementValues)
        {
            if (!replacementValues.ContainsKey(Accessor))
                replacementValues.Add(Accessor, "public");
            if (!replacementValues.ContainsKey(ClassName))
                throw new Exception("Class needs a Name");
        }

        public bool ModifyProjFile(string projName,string classname)
        {
            var Result = false;
            try
            {
                var path = Path.Combine(ProjectPath, projName,$"{projName}.csproj");
                //var project = new Project(path);
                //project.AddItem("Complie", $"{ classname}.cs");
                //project.Save();
                //Result = true;

                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNode root = doc.DocumentElement;
                XmlNode myNode;

                myNode = root.ChildNodes[4].ChildNodes[0];
                var newNode = myNode.Clone();
                newNode.Attributes.Item(0).Value = $"{ classname}.cs";
                root.ChildNodes[4].AppendChild(newNode);

                doc.Save(path);
                Result = true;

            }
            catch (System.Exception ex)
            {
                Result = false;
                throw ex;
            }
            return Result;
        }
    }
}
