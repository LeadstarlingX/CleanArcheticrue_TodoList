using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;


        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserDTO dto)
        {
            var accessToken = await _authenticationService.Login(dto);
            return accessToken != null ? Ok(accessToken) : Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> RefreshToken()
        {
            try
            {
                return Ok(await _authenticationService.RefreshToken());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
