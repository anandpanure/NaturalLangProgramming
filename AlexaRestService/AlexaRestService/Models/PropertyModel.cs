using Natural.Language.TemplateEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    public class PropertyModel : IModel
    {
        public string PropertyName { get; set; }
        public string ReturnType { get; set; }
        public string Accessor { get; set; }
        public string ProjectName { get; set; }
        public string ClassName { get; set; }

        public Response GenerateCode(string input)
        {
            Response response;

            var func = JsonConvert.DeserializeObject<PropertyModel>(input);

            string PropertyName = func.PropertyName;
            string returnType = func.ReturnType;
            string accessor = func.Accessor;
            string projectName = func.ProjectName;
            string className = func.ClassName;
            try
            {
                //string t = string.Format($"created{functionName}");
                //Calling backend Code generation methods.
                Factory factory = new Factory();
                var paramss = new Dictionary<string, string>()
                {
                    {@"@PROPERTY_NAME@",PropertyName },
                    {@"@CLASS_NAME@",className },
                    {@"@ACCESS_LEVEL@",accessor },
                    {@"@RETURN_TYPE@",returnType },
                    {@"@PROJECT_NAME@",projectName }
                };
                var model = factory.PopulateTemplate(ActionType.GenerateProperty.ToString());
                var status = model.PopulateTemplate(TemplateEnum.PropertyTemplate.ToString(), paramss, 0);

                if (status == "success")
                {
                    // call to Json generator..
                    var alexaOutput = $" {status} Created Property {PropertyName} in class {className}.";
                    response = IModel.GetResponseObject(status, alexaOutput);
                }
                else
                {
                    var alexaOutput = "failed: " + status;
                    response = IModel.GetResponseObject("fail", alexaOutput);
                }

            }
            catch (Exception e)
            {
                var alexaOutput = "Exception generated in  Code generator : " + e.Message;
                response = IModel.GetResponseObject("fail", alexaOutput);
            }

            return response;
        }
    }
}
