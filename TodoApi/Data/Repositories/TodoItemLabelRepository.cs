using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Repositories.Base;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    public class TodoItemLabelRepository : BaseRepository<TodoItemLabel>, ITodoItemLabelRepository
    {
        // Casts Context as TodoContext so it doesn't have to be specified in other methods
        public TodoContext TodoContext
        {
            get { return Context as TodoContext; }
        }

        public TodoItemLabelRepository(TodoContext context)
            : base(context){}
    }
}
