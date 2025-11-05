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

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetById(int id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo==null)
            {
                return NotFound("There is not Todo with this ID.");
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(Todo newTodo)
        {
            _todoRepository.AddAsync(newTodo);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound("There is no Todo with this ID.");
            }
            _todoRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, Todo updatedTodo)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound("There is no Todo with this ID.");
            }
            updatedTodo.Id = id;
            _todoRepository.UpdateAsync(updatedTodo);
            return Ok();
        }
    }
}
