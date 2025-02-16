using VerticalFarmingApi.Models.DTO;
using VerticalFarmingApi.Models;

namespace VerticalFarmingApi.Repositories.IRepository
{
    public interface IUserReository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}

