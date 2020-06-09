using System.Collections.Generic;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public IList<TodoItemLabel> TodoItemLabels { get; set; } = new List<TodoItemLabel>();

        public TodoItem()
        {
        }

        public TodoItem(string name, bool isComplete = false)
        {
            Name = name;
            IsComplete = isComplete;
        }
    }
}
