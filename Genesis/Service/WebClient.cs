using RestSharp;
using System.Text.Json;

namespace Genesis.Service
{
    public class WebClient
    {
        readonly RestClient _httpClient;

        public WebClient()
        {
            _httpClient = new RestClient();
        }

        public async Task<RestResponse<R>> SendRequestAsync<T,R>(string url,Method httpMethod, Dictionary<String,String>? headers, T? data)
        {
            var request = new RestRequest(url);
            request.Method = httpMethod;

            if(headers != null && headers.Count > 0)
            {
                foreach(var key in headers.Keys)
                {
                    request.AddHeader(key, headers[key]);
                }
            }

            if(data != null ) { request.AddJsonBody(JsonSerializer.Serialize(data)); }

            return await _httpClient.ExecuteAsync<R>(request);
            
        }
    }
}
