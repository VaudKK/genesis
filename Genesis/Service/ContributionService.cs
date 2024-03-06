using Genesis.data;
using Genesis.Util;

namespace Genesis.Service
{
    public class ContributionService
    {
        private WebClient _webClient;
        private ISecureStorageService _secureStorageService;

        public ContributionService(ISecureStorageService secureStorageService)
        {
            _webClient = new WebClient();
            _secureStorageService = secureStorageService;
        }

        public async Task<List<ContributionsDto>> getContributions()
        {
            var headers = new Dictionary<string, string>();
            var token = await _secureStorageService.Get(GlobalConstants.AUTH_KEY);
            headers.Add("Authorization","Bearer " + token);

            var response = await _webClient.SendRequestAsync<Object,BaseDto<List<ContributionsDto>>>
                ("https://genesis-contribution-17c0a41f1924.herokuapp.com/contributions", RestSharp.Method.Get,headers,null);

            var contributions = new BaseDto<List<ContributionsDto>>();

            if (response.IsSuccessStatusCode)
            {
                contributions = response!.Data;
                return contributions!.data;
            }

            throw new Exception("Error fetching contributions: " + response!.StatusCode + " " + response!.Data);
        }
    }
}
