using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todolistService;


        public TodoListController(ITodoListService services)
        {
            _todolistService = services;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ReturnTodoListDTO>> GetById(int id)
        {
            var temp = await _todolistService.GetByIdAsync(id);
            return temp != null ? Ok(temp) : NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ReturnTodoListDTO>>> GetAllAsync([FromBody]GetTodoListDTO dto)
        {
            try
            {
                var temp = await _todolistService.GetAllAsync(dto);
                return temp != null ? Ok(temp.ToList()) : NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ReturnTodoListDTO>> UpdateAsync([FromBody]GetTodoListDTO dto)
        {
            try
            {
                var result = await _todolistService.UpdateAsync(dto);
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
                var result = await _todolistService.CreateAsync(dto);
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
                await _todolistService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

  
    }
}
