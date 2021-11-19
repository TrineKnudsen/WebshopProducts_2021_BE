using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tsak.WebshopProducts2021.Security;
using Tsak.WebshopProducts2021.Security.Model;
using Tsak.WebshopProducts2021.WebApi.Dtos.Auth;
using Tsak.WebshopProducts2021.WebApi.PolicyHandlers;

namespace Tsak.WebshopProducts2021.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var tokenString = _authService.GenerateJwtToken(new LoginUser
            {
                UserName = dto.Username,
                HashedPassword = _authService.Hash(dto.Password)
            });
            if (string.IsNullOrEmpty(tokenString))
            {
                return BadRequest("Please pass the valid Username and Password");
            }
            return Ok(new { Token = tokenString, Message = "Success" });
        }
        
        
        [HttpGet(nameof(GetProfile))]
        public ActionResult<ProfileDto> GetProfile()
        {
            var permissions = _authService.GetPermissions(1);
            return Ok(new ProfileDto
            {
                Permissions = permissions.Select(p => p.Name).ToList()
            });
        }
    }
}