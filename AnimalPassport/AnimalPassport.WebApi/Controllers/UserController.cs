﻿using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.WebApi.Auth;
using AnimalPassport.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalPassport.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        private readonly IUserManager _userManager;

        public UserController(IAuthService authenticationService, IUserManager userManager)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]AuthModel model)
        {
            var user = await _authenticationService.AuthenticateAsync(model);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // POST: api/Auth
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var m = await _userManager.RegisterAsync(model);

            var user = _authenticationService.Authenticate(m);

            return Ok(user);
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _userManager.GetRolesAsync();

            return Ok(roles);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetPetOwners()
        {
            var users = await _userManager.GetPetOwners();

            return Ok(users);
        }
    }
}