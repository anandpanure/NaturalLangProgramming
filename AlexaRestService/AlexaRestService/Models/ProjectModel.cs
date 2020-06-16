using Natural.Language.TemplateEngine;
using NaturalLangJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    public class ProjectModel : IModel
    {
        public string ProjectName { get; set; }

        public Response GenerateCode(string input)
        {
            Response response;

            var projectDetails = JsonConvert.DeserializeObject<ProjectModel>(input);

            string projectName = projectDetails.ProjectName;

            try
            {

                //Calling backend Code generation methods.

                Factory factory = new Factory();
                var paramss = new Dictionary<string, string>()
                {
                    {@"@PROJECT_NAME@",projectName }
                };
                var model = factory.PopulateTemplate(ActionType.GenerateProject.ToString());
                var status = model.PopulateTemplate(TemplateEnum.ProjectEXETemplate.ToString(), paramss, 0);

                if (status == "success")
                {
                    var jsonObjectFactory = new JsonObjectFactory();
                    var jsonObject = jsonObjectFactory.GetSolution();

                    jsonObject.Name = projectName;
                    jsonObject.Projects[0].Name = projectName;

                    var jsonString = JsonConvert.SerializeObject(jsonObject);

                    var alexaOutput = status + " Created Project " + projectName + " for you";
                    response = IModel.GetResponseObject(status, alexaOutput);

                }
                else
                {
                    var alexaOutput = "failed: " + status;
                    response = IModel.GetResponseObject(status, alexaOutput);
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
