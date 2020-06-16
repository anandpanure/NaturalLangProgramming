using Natural.Language.TemplateEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    public class GenerateUserEntry :IModel
    {
        public string VariableName { get; set; }
        public string ProjectName { get; set; }
        public string ClassName { get; set; }

        public Response GenerateCode(string input)
        {
            Response response;

            var func = JsonConvert.DeserializeObject<GenerateUserEntry>(input);

            string projectName = func.ProjectName;
            string className = func.ClassName;
            string VariableName = func.VariableName;

            string functionName = "Input";
            try
            {
                Factory factory = new Factory();
                var paramss = new Dictionary<string, string>()
                {
                    {@"@FUNCTION_NAME@",functionName },
                    {@"@CLASS_NAME@",className },
                    {@"@PROJECT_NAME@",projectName },
                    {@"@VARIABLE_NAME@",VariableName }
                };
                var model = factory.PopulateTemplate(ActionType.GenerateFunction.ToString());
                var status = model.PopulateTemplate(TemplateEnum.FetchObjectFromConsoleFunctionTemplate.ToString(), paramss, 0);

                var model2 = factory.PopulateTemplate(ActionType.GenerateIntent.ToString());
                var status2 = model2.PopulateTemplate(TemplateEnum.FetchObjectFromConsoleIntentTemplate.ToString(), paramss, 0);

                var model3 = factory.PopulateTemplate(ActionType.GenerateIntent.ToString());
                var status3 = model3.PopulateTemplate(TemplateEnum.StoreToDbContentTemplate.ToString(), paramss, 0);

                if (status == "success")
                {
                    // call to Json generator..
                    var alexaOutput = $"Added entry from console functionality for {className}";
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
