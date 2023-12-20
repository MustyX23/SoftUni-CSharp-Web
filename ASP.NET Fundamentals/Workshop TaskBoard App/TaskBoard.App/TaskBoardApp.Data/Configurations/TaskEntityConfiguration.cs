using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data.Configurations
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            ICollection<Task> tasks = GenerateTasks();

            builder
                .HasOne(t => t.Board)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasData(tasks);
        }
        private ICollection<Task> GenerateTasks()
        {
            ICollection<Task> tasks = new HashSet<Task>() 
            {
                new Task()
                {
                    Title = "Improve CSS styles",
                    Description = "Implementing better styling for all public pages",
                    CreatedOn = DateTime.UtcNow,
                    OwnerId = "7b759171-08e8-4c14-ad92-c364efd9ef32",
                    BoardId = 2
                },
                new Task()
                {
                    Title = "Android Client App",
                    Description = "Create Android Client App for the RESTful TaskBoard service",
                    CreatedOn = DateTime.UtcNow.AddMonths(-5),
                    OwnerId = "430f9c7c-2e27-40b8-bdff-8e54bddb2223",
                    BoardId = 1
                },
                new Task()
                {
                    Title = "Desktop Client App",
                    Description = "Create Desktop Client App for the RESTful TaskBoard service",
                    CreatedOn = DateTime.UtcNow.AddMonths(-2),
                    OwnerId = "7b759171-08e8-4c14-ad92-c364efd9ef32",
                    BoardId = 2
                },
                new Task()
                {
                    Title = "Create Tasks",
                    Description = "Create Tasks Client App for the RESTful TaskBoard service",
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                    OwnerId = "430f9c7c-2e27-40b8-bdff-8e54bddb2223",
                    BoardId = 1
                },
            };

            return tasks;

        }
    }
}
