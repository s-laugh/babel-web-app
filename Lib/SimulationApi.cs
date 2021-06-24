using System;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Newtonsoft.Json.Linq;

namespace babel_web_app.Lib
{
    public class SimulationApi
    {
        private readonly IRestClient _client;

        public SimulationApi(IRestClient client, IOptions<SimulationEngineOptions> optionsAccessor) {
            _client = client;
            _client.BaseUrl = new Uri(optionsAccessor.Value.Url);
            _client.UseNewtonsoftJson();
        }

        public W ExecutePost<W>(string endpoint, object request, string propName) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            restRequest.AddJsonBody(request);
            var response = _client.Post<W>(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new SimulationApiException(response.ErrorMessage);
            }
 
            // Parsing JSON content into element-node JObject
            // var jObject = JObject.Parse(response.Content);
            // var result = jObject.Value<W>(propName);

            return response.Data;
        }

        public W ExecuteGet<W>(string endpoint, string propName) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            var response = _client.Get<W>(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new SimulationApiException(response.ErrorMessage);
            }
 
            // Parsing JSON content into element-node JObject
            //var jObject = JObject.Parse(response.Content);
            //var result = jObject.Value<W>(propName);

            return response.Data;
        }
        
    }
}

