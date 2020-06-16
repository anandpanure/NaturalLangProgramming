using Natural.Language.TemplateEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    public class FunctionModel : IModel
    {
        public string FunctionName { get; set; }
        public string ReturnType { get; set; }
        public string Accessor { get; set; }
        public string ProjectName { get; set; }
        public string ClassName { get; set; }

        public Response GenerateCode(string input)
        {
            Response response;

            var func = JsonConvert.DeserializeObject<FunctionModel>(input);

            string functionName = func.FunctionName;
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
                    {@"@FUNCTION_NAME@",functionName },
                    {@"@CLASS_NAME@",className },
                    {@"@ACCESS_LEVEL@",accessor },
                    {@"@RETURN_TYPE@",returnType },
                    {@"@PROJECT_NAME@",projectName }
                };
                var model = factory.PopulateTemplate(ActionType.GenerateFunction.ToString());
                var status = model.PopulateTemplate(TemplateEnum.FunctionTemplate.ToString(), paramss, 0);

                if(status == "success")
                {
                    // call to Json generator..
                    var alexaOutput = $"Success: Created function <w role=\"amazon:VB\">{functionName}</w> inside class {className}";
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
