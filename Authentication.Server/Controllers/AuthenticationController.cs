using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Authentication.Server.Contracts.Services;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Core.Models;
using Authentication.Shared;
using Authentication.Shared.Contracts.Validators;
using Authentication.Shared.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserValidator _userValidator;
        private readonly IUserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserValidator userValidator, IUserManager<AppUser> userManager, ITokenService tokenService)
        {
            _logger = logger;
            _userValidator = userValidator;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        dynamic ProduceTokenAndUserResponse(AppUser user)
        {
            var token = _tokenService.GenerateToken(Env.Secret, user, Env.TokenLifetime);
            var nUser = user.User;
            return new
            {
                Token = token,
                Application = Env.ApplicationId,
                ExpirationDate = Env.TokenExpirationDate,
                User = new
                {
                    nUser.Name,
                    nUser.Username,
                    nUser.Email,
                    nUser.Active,
                    nUser.UserRoles,
                    nUser.Id,
                    nUser.CreatedDate,
                    nUser.DeletedDate,
                    nUser.Deleted,
                    RedisGuid = Guid.NewGuid()
                }
            };
        }

        [HttpPost("createAccount")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] User user)
        {
            var validation = _userValidator.Validate(user);
            if (validation.IsValid is not true)
                return BadRequest(validation.ToString());

            var newUser = new AppUser(user);

            await _userManager.RegisterAsync(newUser);

            return Ok(ProduceTokenAndUserResponse(newUser));
        }

        [HttpGet("test")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(10));
            return Ok("Hello, Sir!");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var (username, email, password) = request;

            var user = username is not null ? await _userManager.FindByUsernameAsync(username)
                : email is not null ? await _userManager.FindByEmailAsync(email) : null;
            if(user is null)
                return NotFound("User not found");
            var validPassword = await _userManager.IsValidPasswordAsync(user, password);
            if(validPassword is false)
                return StatusCode(StatusCodes.Status403Forbidden, "Invalid password");

            return Ok(ProduceTokenAndUserResponse(user));
        }

        [HttpDelete("deleteAccount")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            await _userManager.DeleteAccountAsync(userId);

            return Ok();
        }
    }
}