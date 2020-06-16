using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    interface IModel
    {
        public Response GenerateCode(string input);

        public static Response GetResponseObject(string status,string alexaOutput)
        {
            var response = new Response
            {
                Result = alexaOutput,
                Status = status
            };
            return response;
        }
    }
}
