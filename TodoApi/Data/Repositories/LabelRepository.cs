using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Repositories.Base;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        // Casts Context as TodoContext so it doesn't have to be specified in other methods
        // TODO: Make Private if necessary
        public TodoContext TodoContext
        {
            get { return Context as TodoContext; }
        }

        public LabelRepository(TodoContext context)
            : base(context) { }
    }
}
