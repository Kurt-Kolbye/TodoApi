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
            //TODO: Get associated labels and return with the TodoItem
            var todoItems = new List<TodoItem>();
            try
            {
                todoItems = _unitOfWork.TodoItems.GetAll().ToList();

                foreach (var todoItem in todoItems)
                {
                    todoItem.Labels = GetLabelsAssignedTo(todoItem);
                }                
            }
            catch (Exception)
            {

                _unitOfWork.Dispose();
            }

            return todoItems;
        }

        public TodoItem Get(long id)
        {
            var todoItem = new TodoItem();
            try
            {
                todoItem = _unitOfWork.TodoItems.Get(id);

                todoItem.Labels = GetLabelsAssignedTo(todoItem);
            }
            catch (Exception)
            {

                _unitOfWork.Dispose();
            }

            return todoItem;
            
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
                var todoItemLabel = new TodoItemLabel
                {
                    TodoItemId = todoItemId,
                    LabelId = labelId
                };

                _unitOfWork.TodoItemLabels.Add(todoItemLabel);

                _unitOfWork.Complete();
                result = true;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            // TODO: Remove debug lines
            // DEBUG START
            var itemLabels = _unitOfWork.TodoItemLabels.GetAll().ToList();

            foreach (var i in itemLabels)
            {
                var itemLabel = itemLabels.FirstOrDefault(il => il.Id == i.Id);
                Debug.WriteLine("TodoService.AddLabel() " + i.Id + " element: \n"
                    + "Id: " + itemLabel.Id + "\n"
                    + "ItemId: " + itemLabel.TodoItemId + "\n"
                    + "LabelId: " + itemLabel.LabelId
                    + "\n----------\n");
            }
            
            // DEBUG END
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
            // TODO: Remove debug line
            //Debug.WriteLine("TodoService.RemoveLabel() output:\n" + _unitOfWork.TodoItemLabels.GetAll().ToString() + "\n");

            return item;
        }

        // Helper method to get labels assigned to a TodoItem
        private IList<Label> GetLabelsAssignedTo(TodoItem todoItem)
        {
            var itemLabels = _unitOfWork.TodoItemLabels.Find(il => il.TodoItemId == todoItem.Id);
            var allLabels = _unitOfWork.Labels.GetAll();

            // Get all labels that are associated with the given TodoItem
            var labels = allLabels.Where(l => itemLabels.Any(il => il.LabelId == l.Id));

            return labels.ToList();
        }
    }
}
