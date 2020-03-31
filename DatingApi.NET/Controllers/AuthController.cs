using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApi.NET.Dto;
using DatingApi.NET.Models;
using DatingApi.NET.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApi.NET.Controllers
{
    [Route("auth/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _config = config;
            _repository = repository;

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto userContract)
        {
            try
            {
                await ValidateRequest(userContract);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
            var user = new User
            {
                UserName = userContract.UserName
            };
            await _repository.Register(user, userContract.Password);
            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            var logged = await _repository.Login(user.UserName, user.Password);
            if (logged == null)
                return Unauthorized();
            var securityToken = GenerateSecurityToken(logged);
            return Ok(new {token=securityToken});
        }

        private string GenerateSecurityToken(User logged)
        {
             var claims = GetClaims(logged);
            var token =_config.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));
            var signingCreds =new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCreds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        private Claim[] GetClaims(User logged)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, logged.Id.ToString()),
                new Claim(ClaimTypes.Name, logged.UserName)
            };
        }

        private async Task ValidateRequest(UserForRegisterDto userContract)
        {
            if (await _repository.UserExists(userContract.UserName))
                throw new ArgumentException("User already exists");
        }
    }
}