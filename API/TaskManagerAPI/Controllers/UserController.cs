using ApplicationCore.Contracts.Service;
using Infrastructure.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Contracts.Manager;
using TaskManagerAPI.DTOs.User;

namespace TaskManagerAPI.Controllers
{

    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: api/User/users
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        { 
            return await _userManager.GetUsersAsync();
        }

        // GET: api/User/{username}
        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<UserDto>> GetUserByUserName(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    return BadRequest("Username cannot be empty");

                var user = await _userManager.GetUserByUserNameAsync(username);

                return user;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/User
        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody]UpdateUserDto userDto )
        {
            try
            {
                await _userManager.UpdateUserAsync(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userManager.DeleteUserAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/User/add-photo
        [HttpPost]
        [Route("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file) 
        {
            try
            {
                if(file == null)
                    return BadRequest("File is empty!"); 

                var photo = await _userManager.AddPhotoAsync(file);

                return photo;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
