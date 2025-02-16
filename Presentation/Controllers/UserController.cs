using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;
using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult<ReturnUserDTO> Login([FromBody]LoginUserDTO dto)
        {
            var user = _userService.Authinticate(dto);
            if (user == null)
                return Unauthorized();

            byte[] bytes = new byte[16];
            BitConverter.GetBytes(user.Id).CopyTo(bytes, 0);
            string token = GenerateJwtToken(new Guid(bytes));
            return Ok(new {token});
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ReturnUserDTO>> GetById(int id)
        {
            var temp = await _userService.GetByIdAsync(id);
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
            try
            {
                var result = await _userService.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        


        private string GenerateJwtToken(Guid userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
