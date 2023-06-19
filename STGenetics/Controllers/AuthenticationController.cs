using Domain.Models;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace STGenetics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IJWTHandler _handler;

        public AuthenticationController(IJWTHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Logs in with the provided credentials.
        /// </summary>
        /// <param name="request">The user credentials.</param>
        /// <returns>The session token if was a succesful login.</returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User request)
        {
            if (request.Login == "string" && request.Password == "string")
            {
                string token = _handler.CreateToken(request.Login);

                return StatusCode(StatusCodes.Status200OK, new { token });
            }

            return StatusCode(StatusCodes.Status401Unauthorized, new { token = string.Empty });
        }
    }
}
