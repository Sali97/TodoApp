using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Interfaces;
using TaskAPI.Models;
using TaskAPI.Repositories;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoRepository _todoRepository;
        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetAllAsync()
        {
            var todos = await _todoRepository.GetAllAsync();
            return Ok(todos);
        }
    }
}
