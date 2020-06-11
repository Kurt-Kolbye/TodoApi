using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    // Relationship model between TodoItem and Label
    public class TodoItemLabel
    {
        public long Id { get; set; }
        public long TodoItemId { get; set; }
        //public TodoItem TodoItem { get; set; }
        public long LabelId { get; set; }
        //public Label Label { get; set; }
    }
}
