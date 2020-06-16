using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace Natural.Language.TemplateEngine
{
    public class TemplatePopulator
    {
        public string ProjectName = "@PROJECT_NAME@";
        public string Accessor = "@ACCESS_LEVEL@";
        public string ReturnType = "@RETURN_TYPE@";
        public string Name = "@FUNCTION_NAME@";
        public string PropertyName = "@PROPERTY_NAME@";
        public string Paramters = "@PARAMETER@";
        public string ClassName = "@CLASS_NAME@";
        public string VariableName = "@VARIABLE_NAME@";
        public string PropertyValue = "@PROPERTY_VALUE@";
        public string SolutionPath = "SolutionPath";
        public string FunctionXMLNode = "function";

        protected const string ProjectPath = @"C:\AdaptivDevelopment\RuntimeTerror";

        protected StringBuilder LoadTemplate(string templateName)
        {
            XmlDocument doc = new XmlDocument();
            var templatePath = Path.Combine(Constants.TemplatePath, $"{ templateName}.xml");
            doc.Load(templatePath);

            StringBuilder functionTemplate = null;
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                functionTemplate = new StringBuilder(HttpUtility.HtmlDecode(node.InnerText));
            }

            return functionTemplate;
        }

        protected StringBuilder Populate(StringBuilder functionTemplate, Dictionary<string, string> rv)
        {
            foreach (var item in rv)
            {
                functionTemplate = functionTemplate.Replace(item.Key, item.Value);
            }

            return functionTemplate;
        }
    }
}
