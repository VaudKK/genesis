using Genesis.converters;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Genesis.Service
{
    public class WebClient
    {
        readonly HttpClient _httpClient;
        readonly JsonSerializerOptions _serializerOptions;

        public JsonSerializerOptions SerializerOptions
        {
            get { return _serializerOptions; }
        }

        public WebClient()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                Converters =
                {
                    new IntConverter(),
                }
            };
        }

        public Task<HttpResponseMessage> SendRequestAsync(string url,HttpMethod httpMethod, string data)
        {
            var httpMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = httpMethod,
                Content = new StringContent(data,Encoding.UTF8,"application/json"),
            };

            return _httpClient
                .SendAsync(httpMessage);
        }
    }
}
