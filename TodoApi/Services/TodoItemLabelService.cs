using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoItemLabelService : ITodoItemLabelService
    {
        private IUnitOfWork _unitOfWork;

        public TodoItemLabelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<TodoItemLabel> GetAll()
        {
            return _unitOfWork.TodoItemLabels.GetAll();
        }

        public TodoItemLabel Get(long id)
        {
            return _unitOfWork.TodoItemLabels.Get(id);
        }

        public bool Add(TodoItemLabel todoItemLabel)
        {
            bool result = false;
            try
            {
                _unitOfWork.TodoItemLabels.Add(todoItemLabel);
                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public bool Update(TodoItemLabel todoItemLabel)
        {
            bool result = false;
            try
            {
                _unitOfWork.TodoItemLabels.Update(todoItemLabel);
                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
               _unitOfWork.Dispose();
            }

            return result;
        }


        public TodoItemLabel Remove(long id)
        {
            var item = _unitOfWork.TodoItemLabels.Get(id);
            try
            {
                if (item != null)
                {
                    _unitOfWork.TodoItemLabels.Remove(item);
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
