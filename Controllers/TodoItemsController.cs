using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApi.Models;
using TodoApi.Models;
using DemoApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using DemoApi.Service;

namespace DemoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ApiControllerBase
    {
        private readonly TodoContext _context;
        private readonly TodoHandler _handler;
        public TodoItemsController(TodoContext context, TodoHandler handler) : base(context)
        {
            _context = context;
            _handler = handler;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            var result = await _context.TodoItems.Select(p => ItemToDto(p)).ToListAsync();
            // return await Response(result);
            return result;

        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDto(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoItemDto)
        {
            var result = await _handler.HandleUpdate(todoItemDto);
            return await Response(result);
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            var result = await _handler.HandleCreate(todoItem);
            return await Response(result);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = await _handler.HandleDelete(id);
            return await Response(result);
        }

        private static TodoItemDto ItemToDto(TodoItem todoItem) =>
        new TodoItemDto
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }
}
