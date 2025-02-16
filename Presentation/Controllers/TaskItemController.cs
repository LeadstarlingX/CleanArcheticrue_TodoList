using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _services;

        public TaskItemController(ITaskItemService service)
        {
            _services = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ReturnTaskItemDTO>> GetById(int id)
        {
            var temp = await _services.GetbyIdAsync(id);
            return temp != null ? Ok(temp) : NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ReturnTaskItemDTO>>> GetAllAsync([FromBody]GetTaskItemDTO dto)
        {
            var temp = await _services.GetAllAsync(dto);
            return temp != null ? Ok(temp.ToList()) : NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReturnTaskItemDTO>> CreateAsync([FromBody]GetTaskItemDTO dto)
        {
            try
            {
                var result = await _services.CreateAsync(dto);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ReturnTaskItemDTO>> UpdateAsync([FromBody]GetTaskItemDTO dto)
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
