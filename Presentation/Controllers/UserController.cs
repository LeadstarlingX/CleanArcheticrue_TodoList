using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize]
        [HttpGet]
        public ActionResult<ReturnUserDTO> GetById()
        {
                var temp = _userService.GetById();
                return temp != null ? Ok(temp) : NotFound();
            
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ReturnUserDTO>>> GetAllAsync([FromBody]GetUserDTO dto)
        {
            var temp = await _userService.GetAllAsync(dto);
            return temp != null ? Ok(temp) : NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ReturnUserDTO>> CreateAsync([FromBody]GetUserDTO dto)
        {
                var result = await _userService.CreateAsync(dto);
                return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ReturnUserDTO>> UpdateAsync([FromBody]ChangePasswordUserDTO dto)
        {
            try
            {
                var result = await _userService.UpdateAsync(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync()
        {
            try
            {
                await _userService.DeleteAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
