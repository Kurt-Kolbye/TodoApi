using TodoApi.Data.Repositories.Base;
using TodoApi.Models;

namespace TodoApi.Data.Repositories
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem> {}
}
