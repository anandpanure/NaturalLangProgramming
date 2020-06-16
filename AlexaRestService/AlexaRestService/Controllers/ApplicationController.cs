using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AlexaRestService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlexaRestService.Controllers
{
    
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ApplicationController : ControllerBase
    {

        [HttpPost]
        [Route("api/Application/generate")]
        public Response GenericActionImplement(ActionModel todo)
        {
            string actionType = todo.Action;
            string input = todo.Input;

            Response response;

            IModel modelType;

            if (actionType == "GenerateProject")
            {
                modelType = new ProjectModel();

            }
            else if (actionType == "GenerateFunction")
            {
                modelType = new FunctionModel();

            }
            else if(actionType == "GenerateSQLCommand")
            {
                modelType = new SQLCommandModel();
            }
            else if (actionType == "GenerateProperty")
            {
                modelType = new PropertyModel();
            }
            else if (actionType == "GenerateClass")
            {
                modelType = new ClassModel();
            }
            else if (actionType == "GenerateStorage")
            {
                modelType = new StorageModel();
            }
            else if (actionType == "FetchDetails")
            {
                modelType = new FetchModel();
            }
            else if (actionType == "GenerateUserEntry")
            {
                modelType = new GenerateUserEntry();
            }
            else if (actionType == "AddFilter")
            {
                modelType = new FilterModel();
            }
            else if (actionType == "DisplayToConsole")
            {
                modelType = new DisplayToConsoleModel();
            }
            else if (actionType == "GetListFromDB")
            {
                modelType = new GetClassList();
            }
            else
            {
                response = new Response
                {
                    Result = "Could not find any rest function for you",
                    Status = "success"
                };

                return response;
            }

            response = modelType.GenerateCode(input);
            return response;
        }

    }
}