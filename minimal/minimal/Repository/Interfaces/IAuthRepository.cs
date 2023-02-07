using minimal.DTO;

namespace minimal.Repository.Interfaces
{
    public interface IAuthRepository
    {
        bool IsUniqueUser(string userName);
        Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginRequest);
        Task<UserDTO> Register(RegistrationRequestDTO registrationRequest);
    }
}
