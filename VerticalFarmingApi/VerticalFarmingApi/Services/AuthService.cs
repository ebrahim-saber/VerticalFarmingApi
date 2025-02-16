using VerticalFarmingApi.Models;
using VerticalFarmingApi.Models.DTO;
using VerticalFarmingApi.Services.IServices;
using VerticalFarmingApi_Utility;

namespace VerticalFarmingApi.Services
{
    public class AuthService: BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string VerticalfarmingUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            VerticalfarmingUrl = configuration.GetValue<string>("ServiceUrls:VerticalfarmingApi");

        }

        public Task<T> LoginAsync<T>(LoginRequestDTO objToCreate)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = objToCreate,
                Url = VerticalfarmingUrl + "/api/UserAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = objToCreate,
                Url = VerticalfarmingUrl + "/api/UserAuth/register"
            });
        }
    }
}
