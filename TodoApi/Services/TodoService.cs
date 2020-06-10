using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    var itemLabels = _unitOfWork.TodoItemLabels.Find(il => il.TodoItemId == id);

                    if (itemLabels != null)
                    {
                        _unitOfWork.TodoItemLabels.RemoveRange(itemLabels);
                    }

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

                _unitOfWork.TodoItemLabels.Add(todoItemLabel);

                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }
            Debug.WriteLine("TodoService.AddLabel() output:\n" + _unitOfWork.TodoItemLabels.GetAll().ToString() + "\n");

            return result;
        }

        public TodoItemLabel RemoveLabel(long todoItemId, long labelId)
        {
            var item = _unitOfWork.TodoItemLabels
                .Find(il => (il.TodoItemId == todoItemId) && (il.LabelId == labelId))
                .FirstOrDefault();

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
            Debug.WriteLine("TodoService.RemoveLabel() output:\n" + _unitOfWork.TodoItemLabels.GetAll().ToString() + "\n");

            return item;
        }
    }
}
