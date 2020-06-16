﻿using Natural.Language.TemplateEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    public class DisplayToConsoleModel :IModel
    {
        public string ProjectName { get; set; }
        public string ClassName { get; set; }

        public string VariableName { get; set; }


        public Response GenerateCode(string input)
        {
            Response response;

            var func = JsonConvert.DeserializeObject<DisplayToConsoleModel>(input);

            string projectName = func.ProjectName;
            string className = func.ClassName;
            string VariableName = func.VariableName;

            string functionName = "ToCsv";
            try
            {
                //string t = string.Format($"created{functionName}");
                //Calling backend Code generation methods.
                Factory factory = new Factory();
                var paramss = new Dictionary<string, string>()
                {
                    {@"@CLASS_NAME@",className },
                    {@"@PROJECT_NAME@",projectName },
                    {@"@VARIABLE_NAME@",VariableName },
                    {@"@FUNCTION_NAME@",functionName }

                };
                var model = factory.PopulateTemplate(ActionType.GenerateFunction.ToString());
                var status = model.PopulateTemplate(TemplateEnum.DisplayObjectContentFunctionTemplate.ToString(), paramss, 0);


                var model2 = factory.PopulateTemplate(ActionType.GenerateIntent.ToString());
                var status2 = model2.PopulateTemplate(TemplateEnum.DisplayObjectContentIntentTemplate.ToString(), paramss, 0);

                if (status == "success")
                {
                    // call to Json generator..
                    var alexaOutput = $"{status}: Program will display data on console for {VariableName}";
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
