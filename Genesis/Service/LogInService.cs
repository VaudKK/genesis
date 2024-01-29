
using Genesis.converters;
using Genesis.data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Genesis.Service
{
    public class LogInService
    {
        private WebClient _webClient;

        public LogInService()
        {
            _webClient = new WebClient();
        }

        public async Task<string> LogIn(LoginDto loginDto)
        {
            var json = JsonSerializer.Serialize(loginDto, _webClient.SerializerOptions);
            var response = await _webClient.SendRequestAsync("https://a112-196-108-179-25.ngrok-free.app/users/login", HttpMethod.Post, json);

            if (response.IsSuccessStatusCode == true)
            {
                var token = JsonSerializer.Deserialize<BaseDto<TokenDto>>(await response.Content.ReadAsStringAsync(),
                    _webClient.SerializerOptions);
                return token!.data.token;

            }

            return "";
        }
    }
}
