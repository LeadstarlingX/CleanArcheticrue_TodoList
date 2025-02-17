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
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService service)
        {
            _taskItemService = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ReturnTaskItemDTO>> GetById(int id)
        {
            var temp = await _taskItemService.GetbyIdAsync(id);
            return temp != null ? Ok(temp) : NotFound();
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ReturnTaskItemDTO>>> GetAllAsync([FromBody]GetTaskItemDTO dto)
        {
            var temp = await _taskItemService.GetAllAsync(dto);
            return temp != null ? Ok(temp.ToList()) : NoContent();
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReturnTaskItemDTO>> CreateAsync([FromBody]GetTaskItemDTO dto)
        {
            try
            {
                var result = await _taskItemService.CreateAsync(dto);
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
                var result = await _taskItemService.UpdateAsync(dto);
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
                await _taskItemService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
