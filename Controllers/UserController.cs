using System.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DemoApi.Dtos;
using DemoApi.Models;
using DemoApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;
using System.Text.Json;

namespace DemoApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly AuthOption _authOptions;
        public UserController(IUserService service, IOptions<AuthOption> authOptionsAccessor)
        {
            _service = service;
            _authOptions = authOptionsAccessor.Value;
        }

        [HttpPost]
        public IActionResult Get([FromBody] UserDto user)
        {

            var loginUser = _service.GetLoginUser(user.Username, user.Password);
            if (loginUser != null)
            {

                var userRole = _service.GetUserRole(loginUser);
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, loginUser.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Roles", userRole != null ? JsonSerializer.Serialize(userRole) : string.Empty,JsonClaimValueTypes.JsonArray)
                };
    
                var token = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    expires: DateTime.Now.AddHours(_authOptions.ExpiresInMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}