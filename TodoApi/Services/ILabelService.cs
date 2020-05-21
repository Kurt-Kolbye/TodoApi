using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ILabelService
    {
        public IEnumerable<Label> GetAll();
        public Label Get(long id);

        public bool Add(Label todoItem);

        public bool Update(Label todoItem);

        public Label Remove(long id);
    }
}