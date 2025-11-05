using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Interfaces;
using TaskAPI.Models;
using TaskAPI.Repositories;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("There does not exist this UserId in the database.");
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(User newUser)
        {
            var user = await _userRepository.GetById(newUser.Id);
            if (user != null)
            {
                return Problem("There is a user with this Id.");
            }
            _userRepository.AddAsync(newUser);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound("This UserId does not exist in the database.");
            }
            _userRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, User newUser)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("This UserId does not exist in the database.");
            }

            newUser.Id = id;
            _userRepository.UpdateAsync(newUser);
            return NoContent();
        }
    }
}
