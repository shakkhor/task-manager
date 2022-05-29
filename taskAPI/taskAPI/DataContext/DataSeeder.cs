using taskAPI.Models;

namespace taskAPI.DataContext
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TasksDataContext>();
            context.Database.EnsureCreated();
            AddSeedData(context);
        }

        private static void AddSeedData(TasksDataContext context)
        {
            var tasks = context.TaskDetails.FirstOrDefault();
            if (tasks != null) return;

            context.TaskDetails.Add(new TaskDetail
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Connect to DB",
                CreatedDate = DateTime.UtcNow,
                Progress = 0,
                Status = TaskStatusEnum.ToDo,
            });
            context.TaskDetails.Add(new TaskDetail
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Connecting to DB",
                CreatedDate = DateTime.UtcNow,
                Progress = 20,
                Status = TaskStatusEnum.InProgress,
            });
            context.TaskDetails.Add(new TaskDetail
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Connected to DB",
                CreatedDate = DateTime.UtcNow,
                Progress = 100,
                Status = TaskStatusEnum.Complete,
            });

            context.SaveChanges();

        }
    }
}
