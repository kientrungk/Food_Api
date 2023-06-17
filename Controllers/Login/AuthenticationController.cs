using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using ApiWebFood.Entities;
using ApiWebFood.Data;
using System.Net;

namespace ApiWebFood.Controllers.Login
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApiDotNetContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationController(ApiDotNetContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [Route("/register")]
        [HttpPost]
        public IActionResult Register(UserRegister user)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var u = new Entities.User { UserName = user.Name, Email = user.Email, PassWord = hashed, Address ="hanoi"};
            _context.Users.Add(u);
            _context.SaveChanges();
            return Ok(new Userdata { Name = user.Name, Email = user.Email, Token = GeneralJWT(u)});
        }

        private string GeneralJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signatureKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signatureKey
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [Route("Profile")]
        [HttpGet]
        public IActionResult Profile()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity!= null)
            {
                var userClaims =  identity.Claims;
                var Id = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                Userdata data = new Userdata
                {
                    Id = Convert.ToInt32(Id),
                    Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                };
                return Ok(User);
            }
            return Unauthorized();
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login(UserLogin login)
        {
            try
            {
                var UserLogin = _context.Users.Where(u => u.Email.Equals(login.Email)).First();
                if (UserLogin == null)
                {
                    return NotFound();
                }
                bool verycode = BCrypt.Net.BCrypt.Verify(login.Password, UserLogin.PassWord);
                if (!verycode)
                {
                    return Unauthorized();
                }
                return Ok(new Userdata { Id = UserLogin.Id, Name = UserLogin.UserName, Email = UserLogin.Email, Token = GeneralJWT(UserLogin) }); ;
            }
            catch (Exception)
            {
                throw;
                return Unauthorized(login);
            }
        }
    }
}
