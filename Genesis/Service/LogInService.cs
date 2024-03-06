using Genesis.data;
using Genesis.Util;

namespace Genesis.Service
{
    public class LogInService
    {
        private WebClient _webClient;
        private readonly ISecureStorageService _secureStorageService;

        public LogInService(ISecureStorageService secureStorageService)
        {
            _webClient = new WebClient();
            _secureStorageService = secureStorageService;
        }

        public async Task<string> LogIn(LoginDto loginDto)
        {

            var response = await _webClient.SendRequestAsync<LoginDto,BaseDto<TokenDto>>("https://genesis-auth-17ff2f97ba74.herokuapp.com/users/login",
                RestSharp.Method.Post,null, loginDto);

            if (response.IsSuccessStatusCode == true)
            {
                var token = response?.Data?.data.token;
                if(token != null)
                {
                    await _secureStorageService.Save(GlobalConstants.AUTH_KEY, token);
                    return GlobalConstants.OKAY;
                }
            }

            return "";
        }
    }
}
