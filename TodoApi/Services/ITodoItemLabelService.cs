using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoItemLabelService
    {
        public IEnumerable<TodoItemLabel> GetAll();
        public TodoItemLabel Get(long id);

        public bool Add(TodoItemLabel todoItemLabel);

        public bool Update(TodoItemLabel todoItemLabel);

        public TodoItemLabel Remove(long id);
    }
}