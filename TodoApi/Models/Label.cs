using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Label
    {

        public long Id { get; set; }
        public string Name { get; set; }

        public IList<TodoItemLabel> TodoItemLabels { get; set; } = new List<TodoItemLabel>();
    }
}
