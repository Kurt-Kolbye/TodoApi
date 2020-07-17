using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        [NotMapped]
        public IList<Label> Labels { get; set; }

        public TodoItem()
        {
        }

        public TodoItem(string name, bool isComplete = false)
        {
            Name = name;
            IsComplete = isComplete;
            Labels = new List<Label>();
        }
    }
}
