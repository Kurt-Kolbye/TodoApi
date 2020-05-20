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
        
        public IEnumerable<TodoItem> GetAll()
        {
            return _unitOfWork.TodoItems.GetAll();
        }

        public TodoItem Get(long id)
        {
            return _unitOfWork.TodoItems.Get(id);
        }

        public bool Add(TodoItem todoItem)
        {
            bool result = false;
            try
            {
                _unitOfWork.TodoItems.Add(todoItem);
                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public bool Update(TodoItem todoItem)
        {
            bool result = false;
            try
            {
                // TODO: Verify this works or if there's other logic that needs to occur to update an item
                var item = _unitOfWork.TodoItems.Get(todoItem.Id);
                if (item != null)
                {
                    item = todoItem;
                    _unitOfWork.Complete();
                    result = true;
                }
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return result;
        }


        public TodoItem Remove(TodoItem todoItem)
        {
            var item = _unitOfWork.TodoItems.Get(todoItem.Id);
            try
            {
                if (item != null)
                {
                    _unitOfWork.TodoItems.Remove(todoItem);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return item;
        }
    }
}
