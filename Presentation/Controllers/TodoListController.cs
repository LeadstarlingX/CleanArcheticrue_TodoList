using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Core.DTO;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _services;

        public TodoListController(ITodoListService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ReturnTodoListDTO>> GetById(int id)
        {
            var temp = await _services.GetByIdAsync(id);
            return temp != null ? Ok(temp) : NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ReturnTodoListDTO>>> GetAllAsync([FromBody]GetTodoListDTO dto)
        {
            var temp = await _services.GetAllAsync(dto);
            return temp != null ? Ok(temp.ToList()) : NoContent();
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ReturnTodoListDTO>> UpdateAsync([FromBody]GetTodoListDTO dto)
        {
            try
            {
                var result = await _services.UpdateAsync(dto);
                return Ok(result);
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReturnTodoListDTO>> CreateAsync([FromBody]GetTodoListDTO dto)
        {
            try
            {
                var result = await _services.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _services.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
