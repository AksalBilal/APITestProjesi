using System;
using System.Configuration;
using RestSharp;

namespace TechTestAPI.Base.Http
{
    public class RestHttpClient
    {
        private static RestClient _client;
        private static RestRequest _request;

        public static RestHttpClient Create()
        {
            _client = new RestClient();
            _request = new RestRequest();
            return new RestHttpClient();
        }

        public IRestResponse Get<T>(Parameter parameters) where T : new()
        {
            try
            {
                _client.BaseUrl = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);//istek atılacak url app.config den çekilir
                _request.AddParameter(parameters);
                _request.AddParameter("apikey", ConfigurationManager.AppSettings["apikey"]);// apı key app.config den çekilerek parametre olarak eklenir
                var httpResponseMessage = _client.Execute<T>(_request);

                return httpResponseMessage;
                // return JsonConvert.DeserializeObject<T>(httpResponseMessage.Content);// direkt olarak dönen sonucu serialize edip döndermememin sebebi data nın yanında http status code u da kontrol edebilmek.
            }
            catch
            {
                return new RestResponse();
            }
        }
        // Post methodunu senaryoda kullanmadık örnek olsun diye yazdım.
        public T Post<T>(Parameter parameters, RestRequest request) where T : new()
        {
            try
            {
                request.Resource = "posts";
                request.Method = Method.POST;
                _client.BaseUrl = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                request.AddParameter("apikey", ConfigurationManager.AppSettings["apikey"]);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                var response = _client.Execute<T>(request);
                if (response.ErrorException != null)
                {
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var browserStackException = new ApplicationException(message, response.ErrorException);
                    throw browserStackException;
                }

                return response.Data;
            }
            catch
            {
                return new T();
            }
        }

        /*
         * PUT
         * DELETE
         * UPDATE 
         * seklinde çoğaltılabilir...
         *
         */
    }
}