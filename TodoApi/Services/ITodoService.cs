using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        public IEnumerable<TodoItem> GetAll();
        public TodoItem Get(long id);

        public bool Add(TodoItem todoItem);

        public bool Update(TodoItem todoItem);

        public TodoItem Remove(TodoItem todoItem);
    }
}