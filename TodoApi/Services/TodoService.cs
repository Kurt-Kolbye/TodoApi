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

        // TODO: Pull out into a new TodoItemLabel service
        public bool AddLabel(long todoItemId, long labelId)
        {
            bool result = false;

            try
            {
                var todoItem = _unitOfWork.TodoItems.Get(todoItemId);
                var label = _unitOfWork.Labels.Get(labelId);

                var todoItemLabel = new TodoItemLabel
                {
                    Label = label,
                    LabelId = label.Id,
                    TodoItem = todoItem,
                    TodoItemId = todoItem.Id
                };

                // Add new record for the TodoItemLabel
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

        public bool Update(TodoItem todoItem)
        {
            bool result = false;
            try
            {
                _unitOfWork.TodoItems.Update(todoItem);
                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
               _unitOfWork.Dispose();
            }

            return result;
        }


        public TodoItem Remove(long id)
        {
            var item = _unitOfWork.TodoItems.Get(id);
            try
            {
                if (item != null)
                {
                    _unitOfWork.TodoItems.Remove(item);
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
