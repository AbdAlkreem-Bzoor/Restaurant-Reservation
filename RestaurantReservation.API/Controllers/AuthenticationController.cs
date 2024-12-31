using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestaurantReservation.API.Models.User;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.API.Services;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/v{version:apiVersion}/authentication")]
    [ApiController]
    [ApiVersion(1.0)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IApplicationRepository _repository;
        private readonly IValidator<RegisterRequestDto> _registerRequestDtoValidator;
        private readonly IValidator<LoginRequestDto> _loginRequestDtoValidator;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(IJwtTokenGenerator jwtTokenGenerator,
                                        IApplicationRepository repository,
                                        IMapper mapper,
                                        ILogger<AuthenticationController> logger,
                                        IValidator<RegisterRequestDto> registerRequestDtoValidator,
                                        IValidator<LoginRequestDto> loginRequestDtoValidator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _registerRequestDtoValidator = registerRequestDtoValidator;
            _loginRequestDtoValidator = loginRequestDtoValidator;
        }
        [HttpGet]
        public ActionResult<bool> ValidateToken(string token)
        {
            return Ok(_jwtTokenGenerator.ValidateToken(token));
        }
        /// <summary>
        /// Processes a login request.
        /// </summary>
        /// <param name="loginRequest">Login request data.</param>
        /// <response code="200">JWT token.</response>
        /// <response code="400">If the login credentials are invalid.</response>
        /// <response code="401">When user with the provided credentials does not exist.</response>
        [HttpPost("authenticate/login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login(LoginRequestDto loginRequest)
        {
            var result = await _loginRequestDtoValidator.ValidateAsync(loginRequest);
            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            var user = await _repository.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            if (user is null)
            {
                return Unauthorized();
            }

            return Ok(await _jwtTokenGenerator.GenerateToken(user.Id));
        }
        [HttpPost("authenticate/register-customer")]
        public async Task<ActionResult<string?>> RegisterCustomer(RegisterRequestForCustomerDto registerRequest)
        {
            if (await _repository.IsCustomerExistByEmail(registerRequest.Email))
            {
                return BadRequest("Customer already exists.");
            }

            var user = _mapper.Map<User>(registerRequest);

            user.Role = Db.Entities.UserRole.Customer;

            await _repository.AddUserAsync(user);

            await _repository.AddCustomerAsync(await _repository.GetCustomerByEmailAsync(user.Email) ?? throw new Exception());

            await _repository.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(_mapper.Map<UserWithoutPasswordDto>(user));

            return Ok(token);
        }
        /// <summary>
        /// Processes registering a normal user request.
        /// </summary>
        /// <param name="registerRequest">Registering request data.</param>
        /// <returns>JWT token</returns>
        /// <response code="200">JWT token.</response>
        /// <response code="400">If the register data are invalid or the user name is duplicated.</response>
        [HttpPost("authenticate/register-user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string?>> RegisterUser(RegisterRequestDto registerRequest)
        {
            var result = await _registerRequestDtoValidator.ValidateAsync(registerRequest);
            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            if (await _repository.IsUserExistByUserName(registerRequest.UserName))
            {
                return BadRequest("Username already exists.");
            }

            var user = _mapper.Map<User>(registerRequest);

            user.Role = Db.Entities.UserRole.None;

            await _repository.AddUserAsync(user);

            await _repository.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(_mapper.Map<UserWithoutPasswordDto>(user));

            return Ok(token);
        }
        /// <summary>
        /// Processes registering an admin user request.
        /// </summary>
        /// <param name="registerRequestDto">Registering request data.</param>
        /// <returns>JWT token</returns>
        /// <response code="200">JWT token.</response>
        /// <response code="400">If the register data are invalid or the user name is duplicated.</response>
        [HttpPost("authenticate/register-admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string?>> RegisterAdmin(RegisterRequestDto registerRequestDto)
        {
            if (await _repository.IsUserExistByUserName(registerRequestDto.UserName))
            {
                return BadRequest("Username already exists.");
            }

            var user = _mapper.Map<User>(registerRequestDto);

            user.Role = Db.Entities.UserRole.Admin;

            await _repository.AddUserAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(_mapper.Map<UserWithoutPasswordDto>(user));

            return Ok(token);
        }
    }
}
