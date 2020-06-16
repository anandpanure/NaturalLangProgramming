using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaRestService.Models
{
    public class SQLCommandModel : IModel
    {
        public string CommandType { get; set; }

        public string QueryText { get; set; }

        public Response GenerateCode(string input)
        {
            Response response;

            var sqlCommand = JsonConvert.DeserializeObject<SQLCommandModel>(input);

            string commandType = sqlCommand.CommandType;
            string queryText = sqlCommand.QueryText;
            //string whereClause = sqlCommand.WhereClause;
            try
            {

                string query = queryText.Replace("star", "*");
                //string finalwhere = finalwhere.StartsWith("where");
                //Calling backend Code generation methods.

                response = new Response
                {
                    Result = "Created " + commandType + " with query '" + query + "' for you",
                    Status = "success"
                };
            }
            catch (Exception e)
            {

                response = new Response
                {
                    Result = "Exception generated in " + commandType + " Code generator : " + e.Message,
                    Status = "fail"
                };
            }

            return response;
        }
    }
}
