﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Repositories;

namespace TodoApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItems { get; }
        ILabelRepository Labels { get; }
        ITodoItemLabelRepository TodoItemLabels { get; }
        
        int Complete();
    }
}
