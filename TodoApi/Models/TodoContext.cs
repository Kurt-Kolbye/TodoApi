using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            // Configure Entity to have many-to-many relationship between TodoItems and Labels
            //modelBuilder.Entity<TodoItemLabel>()
            //                .HasKey(til => new { til.TodoItemId, til.LabelId });
            //modelBuilder.Entity<TodoItemLabel>()
            //                .HasOne(til => til.TodoItem)
            //                .WithMany(ti => ti.TodoItemLabels)
            //                .HasForeignKey(til => til.TodoItemId);
            //modelBuilder.Entity<TodoItemLabel>()
            //                .HasOne(til => til.Label)
            //                .WithMany(ti => ti.TodoItemLabels)
            //                .HasForeignKey(til => til.LabelId);
        //}

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Label> Labels { get; set; }

        // Relation between TodoItems and Labels
        //public DbSet<TodoItemLabel> TodoItemLabels { get; set; }
    }
}
