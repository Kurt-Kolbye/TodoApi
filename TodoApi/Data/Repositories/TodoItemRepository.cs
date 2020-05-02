using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Repositories.Base;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        // Casts Context as TodoContext so it doesn't have to be specified in other methods
        // TODO: Make Private if necessary
        public TodoContext TodoContext
        {
            get { return Context as TodoContext; }
        }
        public TodoItemRepository(TodoContext context)
            : base(context){}

        // TODO: Implement other methods from ITodoItemRepository and Repository

        

    }
}
