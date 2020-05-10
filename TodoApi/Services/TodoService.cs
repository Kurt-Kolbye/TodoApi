using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private IUnitOfWork _unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // TODO: Implement methods
        public IList<TodoItem> GetAllTodoItems()
        {
            throw new NotImplementedException();
        }

        public TodoItem GetTodoItem(long id)
        {
            throw new NotImplementedException();
        }

        public bool CreateTodoItem(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTodoItem(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }


        public TodoItem RemoveTodoItem(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        // TODO: Remove if never used
        private bool TodoItemExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}
