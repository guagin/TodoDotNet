using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Model;

namespace Todo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly TodoItemRepository _todoItemRepository;
        public TodoController(ILogger<TodoController> logger, TodoItemRepository todoItemRepository)
        {
            _logger = logger;
            _todoItemRepository = todoItemRepository;
        }

        [HttpGet("")]
        public Task<List<TodoItem>> ListAllItems()
        {
            return _todoItemRepository.SelectAll();
        }

        [HttpGet("{id}")]
        public Task<TodoItem> GetTodoItem(long id)
        {
            return _todoItemRepository.Select(id);
        }

        [HttpPut("{id}")]
        public async Task<bool> ChangeName(long id, TodoItem item)
        {
            TodoItem itemToUpdate = await _todoItemRepository.Select(id);
            itemToUpdate.Name = item.Name;
            itemToUpdate.IsCompleted = item.IsCompleted;

            return await _todoItemRepository.Update(item);
        }


        [HttpPost("{id}")]
        public async Task<long> Create(TodoItem todoItem)
        {
            await _todoItemRepository.Create(todoItem);
            return todoItem.Id;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(long id)
        {
            return await _todoItemRepository.Delete(id);
        }
    }
}
