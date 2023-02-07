using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using minimal.Data;
using minimal.DTO;
using minimal.Models;
using minimal.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace minimal.Repository.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private string secretKey;
        public AuthRepository(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;       
            _mapper = mapper;
            _configuration = configuration;
            secretKey= _configuration.GetValue<string>("ApiSettings:Secret");
        }
        public async Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginRequest)
        {
            var user = _context.LocalUsers.FirstOrDefault(x => x.UserName == loginRequest.UserName && x.Password == loginRequest.Password);
            if (user != null && !string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password)) 
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key=Encoding.ASCII.GetBytes(secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Role),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                };
                var token=tokenHandler.CreateToken(tokenDescriptor);
                var loginResponseDTO = new LoginResponseDTO()
                {
                    User = _mapper.Map<UserDTO>(user),
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
                return loginResponseDTO;
            }
            return null;
        }

        public bool IsUniqueUser(string userName)
        {
            return !_context.LocalUsers.Where(x => x.UserName == userName).Any();
        }

        public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequest)
        {
            var user = _mapper.Map<LocalUser>(registrationRequest);
            user.Role = "admin";
            _context.LocalUsers.Add(user);
            _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }
    }
}
