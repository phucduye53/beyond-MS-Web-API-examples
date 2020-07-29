using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApi.Models;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace DemoApi.Service
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetAll();
        Task<TodoItem> Get(long id);

        Task Update(TodoItem item);

        Task Create(TodoItem item);

        Task<TodoItem> Delete(long id);

    }
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;
        public TodoItemService(TodoContext context)
        {
            _context = context;
        }
        public Task Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            return _context.SaveChangesAsync();
        }

        public async Task<TodoItem> Delete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> Get(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return null;
            }

            return todoItem;
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task Update(TodoItem item)
        {
            var todoItem = await _context.TodoItems.FindAsync(item.Id);
            todoItem.Name = item.Name;
            todoItem.IsComplete = item.IsComplete;
            try
            {
               await _context.SaveChangesAsync();
            
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(item.Id))
                {
                    // DO SHIT
                }
                else
                {
                    throw;
                }
            }
        }
        public bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}