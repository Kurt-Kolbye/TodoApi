﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Repositories;

namespace TodoApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoContext _context;

        public ITodoItemRepository TodoItems { get; private set; }
        public ILabelRepository Labels { get; private set; }
        public ITodoItemLabelRepository TodoItemLabels { get; private set; }
        
        public UnitOfWork(TodoContext context)
        {
            _context = context;
            TodoItems = new TodoItemRepository(_context);
            Labels = new LabelRepository(_context);
            TodoItemLabels = new TodoItemLabelRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
