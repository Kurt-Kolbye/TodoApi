using TodoApi.Data.Repositories.Base;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        // Add some specific methods here. Ideas listed below (should be in service layer?)

        //AddLabelToTodoItem
        //AddLabels

        //GetLabel
        //GetTodoItemsWithLabels(IList<Label> labels)


        //HasLabel
        //HasLabels

    }
}
