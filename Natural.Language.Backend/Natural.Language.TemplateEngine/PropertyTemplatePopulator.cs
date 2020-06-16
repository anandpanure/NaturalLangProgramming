using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Natural.Language.TemplateEngine
{
    class PropertyTemplatePopulator : TemplatePopulator, IPopulateTemplate
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
                StringBuilder PropertyTemplate = LoadTemplate(templateName);

                if (!CheckProjectExistance(rv[@"@PROJECT_NAME@"]))
                    return "Project File Doesn't exists";

                string path = Path.Combine(ProjectPath, rv[ProjectName], $"{rv[ClassName]}.cs");

                if (!File.Exists(path))
                    return $"{rv[ClassName]} Class not available";

                string NewContent;
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (StreamReader sw = new StreamReader(fs))
                    {
                        var currentContent = sw.ReadToEnd();

                        injectAtLine = currentContent.IndexOf("{", currentContent.IndexOf('{') + 1) + 1;

                        if (currentContent.Contains(rv[PropertyName] + "()" + Environment.NewLine)
                            || currentContent.Contains(rv[PropertyName] + "(){"))
                            return "Function already exists";
                        if (currentContent.Contains(rv[PropertyName] + " {get"))
                            return $"Property {rv[PropertyName]} already exists";

                        PopulateDefaults(rv);
                        NewContent = currentContent.Insert(injectAtLine, Populate(PropertyTemplate, rv).ToString());
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

        protected void PopulateDefaults(Dictionary<string, string> replacementValues)
        {
            if (!replacementValues.ContainsKey(Accessor))
                replacementValues.Add(Accessor, "public");
            if (!replacementValues.ContainsKey(ReturnType))
                replacementValues.Add(ReturnType, "void");
            if (!replacementValues.ContainsKey(Paramters))
                replacementValues.Add(Paramters, string.Empty);
            if (!replacementValues.ContainsKey(PropertyName))
                throw new Exception("Property needs a Name");
        }
    }
}
