using System;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace csharp
{
    public class Function
    {
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = new APIGatewayProxyResponse();

            try
            {
                var responseBody = new Dictionary<string, string>
                {
                    { "message", "Hello, world!" }
                };

                var responseJson = JsonSerializer.Serialize(responseBody);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" }
                };
                response.Body = responseJson;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Body = ex.ToString();
            }

            return response;
        }
    }
}
