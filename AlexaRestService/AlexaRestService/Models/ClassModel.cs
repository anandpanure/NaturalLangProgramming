using Natural.Language.TemplateEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    public class ClassModel : IModel
    {
        public string ClassName { get; set; }
        public string Accessor { get; set; }
        public string ProjectName { get; set; }

        public Response GenerateCode(string input)
        {
            Response response;

            var classDetails = JsonConvert.DeserializeObject<ClassModel>(input);

            string className = classDetails.ClassName;
            string accessor = classDetails.Accessor;
            string projectName = classDetails.ProjectName;

            try
            {

                //Calling backend Code generation methods.
                Factory factory = new Factory();
                var paramss = new Dictionary<string, string>()
                {
                    {@"@ACCESS_LEVEL@",accessor },
                    {@"@CLASS_NAME@",className },
                    {@"@PROJECT_NAME@",projectName }
                };
                var model = factory.PopulateTemplate(ActionType.GenerateClass.ToString());
                var status = model.PopulateTemplate(TemplateEnum.ClassTemplate.ToString(), paramss, 0);

                if(status == "success")
                {
                    var alexaOutput = status + $": Created Class {className} for you";
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
