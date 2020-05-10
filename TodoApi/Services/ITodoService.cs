using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        public IList<TodoItem> GetAllTodoItems();
        public TodoItem GetTodoItem(long id);

        public bool CreateTodoItem(TodoItem todoItem);

        public bool UpdateTodoItem(TodoItem todoItem);

        public TodoItem RemoveTodoItem(TodoItem todoItem);
    }
}