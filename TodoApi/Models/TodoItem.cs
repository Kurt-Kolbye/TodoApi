using System.Collections.Generic;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public IList<Label> Labels { get; set; }

        public TodoItem()
        {
            Labels = new List<Label>();
        }

        public TodoItem(string name, bool isComplete = false)
        {
            Name = name;
            IsComplete = isComplete;
            Labels = new List<Label>();
        }

        //public IList<TodoItemLabel> TodoItemLabels { get; set; }
    }
}
