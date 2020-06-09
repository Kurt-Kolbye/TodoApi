using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Label> Labels { get; set; }

        // Relation between TodoItems and Labels
        public DbSet<TodoItemLabel> TodoItemLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure Entity to have many - to - many relationship between TodoItems and Labels
            modelBuilder.Entity<TodoItemLabel>()
                .HasKey(item => new { item.TodoItemId, item.LabelId });
        }
    }
}
