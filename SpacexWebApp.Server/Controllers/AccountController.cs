using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpacexWebApp.Server.Data;
using SpacexWebApp.Server.Data.Models;
using SpacexWebApp.Server.Services;
using System.IdentityModel.Tokens.Jwt;

namespace SpacexWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHandler _jwtHandler;
        public AccountController(ApplicationDbContext context, JwtHandler jwtHandler) 
        {
            _context = context;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ApiLoginRequest apiLoginRequest)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.Equals(apiLoginRequest.Email));
            if (user == null || !PasswordService.VerifyPassword(apiLoginRequest.Password, user.Password))
                return Unauthorized(new ApiLoginResult()
                {
                    Success = false,
                    Message = "Invalid Email or Password."
                });

            return Ok(new ApiLoginResult()
            {
                Success = true,
                Message = "Login successful",
                Token = _jwtHandler.GenerateToken(user),
            });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(ApiRegisterRequest apiRegisterRequest)
        {
            try
            {
                if(_context.Users.FirstOrDefault(x => x.Email.Equals(apiRegisterRequest.Email)) != null)
                {
                    return Conflict(new { Message = "Email already in use." });
                }

                var saltBytes = PasswordService.GenerateSalt();
                var hashedPassword = PasswordService.HashPassword(apiRegisterRequest.Password, saltBytes);
                var user = new User
                {
                    FirstName = apiRegisterRequest.FirstName,
                    LastName = apiRegisterRequest.Lastname,
                    Email = apiRegisterRequest.Email,
                    Password = hashedPassword
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();                

                return Ok(new ApiRegisterResult()
                {
                    Success = true,
                    Message = "User added!"
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {ex.Message });
            }
        }

    }
}
