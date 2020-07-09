using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        public IEnumerable<TodoItem> GetAll();
        public TodoItem Get(long id);

        public bool Add(TodoItem todoItem);
        public bool AddLabel(long todoItemId, long labelId);

        public bool Update(TodoItem todoItem);

        public TodoItem Remove(long id);

        public TodoItemLabel RemoveLabel(long todoItemId, long labelId);
    }
}