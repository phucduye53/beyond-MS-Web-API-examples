using System.Threading.Tasks;
using DemoApi.Dtos;
using DemoApi.Models;

namespace DemoApi.Service
{
    public class TodoHandler
    {
        private readonly ITodoItemService _todoItemService;

        public TodoHandler(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IBaseCommandResult> HandleGet(long id)
        {

            var todoItem = await _todoItemService.Get(id);

            if (todoItem == null)
            {
                return new BaseCommandResult(false, "TodoItem not found", null);
            }
            // MAPPING AND RETURN RESULT
            return new BaseCommandResult(true, "TodoItem get with Success!", ItemToDto(todoItem));
        }
        public async Task<IBaseCommandResult> HandleCreate(TodoItem item)
        {
            await _todoItemService.Create(item);

            return new BaseCommandResult(true, "TodoItem created with Success!", ItemToDto(item));
        }
        public async Task<IBaseCommandResult> HandleUpdate(TodoItemDto item)
        {

            var todoItem = await _todoItemService.Get(item.Id);

            if (todoItem == null)
            {
                return new BaseCommandResult(false, "TodoItem not found", null);
            }
            else
            {
                await _todoItemService.Update(todoItem);
                return new BaseCommandResult(true, "TodoItem updated with Success!", ItemToDto(todoItem));
            }


        }
        public async Task<IBaseCommandResult> HandleDelete(long id)
        {

            var todoItem = await _todoItemService.Delete(id);
            if (todoItem == null)
            {
                return new BaseCommandResult(false, "TodoItem not found", null);
            }
            else
            {
        
                return new BaseCommandResult(true, "TodoItem deleted with Success!", ItemToDto(todoItem));
            }


        }
        public static TodoItemDto ItemToDto(TodoItem todoItem) =>
        new TodoItemDto
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }
}