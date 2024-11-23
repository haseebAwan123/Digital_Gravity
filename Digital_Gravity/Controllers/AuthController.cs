using Digital_Gravity.Authentication;
using Digital_Gravity.Models;
using Digital_Gravity.Repositories;
using Digital_Gravity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Digital_Gravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IRepository<Users> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IRepository<Users> userRepository, IConfiguration configuration, JwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = (await _userRepository.GetAllAsync())
                       .FirstOrDefault(u => u.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return Unauthorized();

            var token = _jwtTokenService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }


    }
}
