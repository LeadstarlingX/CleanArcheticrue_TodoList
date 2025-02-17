using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.DTO;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Data.Entities;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IGenericRepository<User> userRepository,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        
        public async Task<string?> Login(LoginUserDTO dto)
        {
            var temp = await _userRepository.FindByConditionAsync(x => x.UserName == dto.UserName);
            if (!temp.Any())
                return null;
            var x = temp.ToList().First();
            bool verify = BCrypt.Net.BCrypt.Verify(dto.PasswordHash, x.PasswordHash);
            if (!verify)
                return null;
            x.RefreshToken = Guid.NewGuid().ToString();
            await _userRepository.UpdateAsync(x);
            return GenerateJwtToken(x.Id);
        }


        public async Task<string?> RefreshToken()
        {
            int id = RetrieveUserID();
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;
            string refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(user);
            return refreshToken;
        }

        public int RetrieveUserID()
        {
            string authorizationHeader = _httpContextAccessor.HttpContext!.Request.Headers.Authorization!;
            if (authorizationHeader == null)
                throw new Exception("No authorization was found in the header");
            string token = authorizationHeader.Substring("Bearer".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            Claim userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "userId")!;

            if (userIdClaim == null)
                throw new Exception("UserId not found in the token.");

            if (!int.TryParse(userIdClaim.Value, out int id))
                throw new Exception("Invalid UserId format in token.");

            return id;
        }

        public string AccessTokenString()
        {
            int id = RetrieveUserID();
            return GenerateJwtToken(id);
        }

        private string GenerateJwtToken(int userId)
        {
            var claims = new[]
            {
                new Claim("userId", userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:LifeTime"]!)),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
