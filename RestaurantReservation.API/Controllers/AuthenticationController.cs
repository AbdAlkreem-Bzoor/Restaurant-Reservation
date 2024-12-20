using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthenticationController(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        }
        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(User user)
        {
            var token = await _jwtTokenGenerator.GenerateToken(user.Id);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
        [HttpGet]
        public ActionResult<bool> ValidateToken(string token)
        {
            return Ok(_jwtTokenGenerator.ValidateToken(token));
        }
    }
}
