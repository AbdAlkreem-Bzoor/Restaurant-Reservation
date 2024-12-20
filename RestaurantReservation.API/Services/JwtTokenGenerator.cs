using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.API.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservation.API.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IRestaurantReservationRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtTokenGenerator> _logger;
        public JwtTokenGenerator(IConfiguration configuration,
                                 ILogger<JwtTokenGenerator> logger, 
                                 IRestaurantReservationRepository repository)
        {
            _configuration = configuration;
            _logger = logger;
            _repository = repository;
        }
        public async Task<string?> GenerateToken(int id)
        {
            var user = await ValidateUserCredentials(id);

            if (user is null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"] ?? throw new Exception()));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>()
            {
                new("user_name", user.UserName),
                new("email", user.Email),
                new("password", user.Password),
                new("role", user.Role.ToString())
            };

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claimsForToken,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }
        private async Task<User?> ValidateUserCredentials(int id)
        {
            return await _repository.GetUserAsync(id);
        }
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"] ?? throw new Exception());

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    return true;
                }

                return false;
            }
            catch (SecurityTokenExpiredException)
            {
                _logger.LogInformation("Token has expired");
                return false;
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning($"Token validation failed: {ex.Message}");
                return false;
            }
            catch (Exception)
            {
                _logger.LogError("Something went wrong!");
                return false;
            }
        }
    }
}
