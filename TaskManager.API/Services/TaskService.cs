using TaskManager.API.Models;
using Task = TaskManager.API.Models.Task;

namespace TaskManager.API.Services
{
    
    public class TaskService : ITaskService
    {
        private static List<Task> _tasks = new List<Task> { 
         
            new Task { Id = Guid.Parse("78d02206-5584-4291-b4f3-9478671653a7"), Title = "Tarea 1",  Description = "Descripción de la tarea 1", IsCompleted = false },
            new Task { Id = Guid.Parse("908bc7be-46b4-48bd-9804-7763c6e481db"), Title = "Tarea 2", Description = "Descripción de la tarea 2", IsCompleted = true }
        };

        public event ChangeStatusHandler? OnChangeStatus;

        public IEnumerable<Task> GetTasks() => _tasks;
        public Task? GetById(Guid Id)=> _tasks.FirstOrDefault(x => x.Id == Id);
        public Task Create(Task newTask)
        {
            newTask.Id = Guid.NewGuid();
            _tasks.Add(newTask);
            return newTask;
        }

        public Task? UpdateStatus(Guid id, bool completed)
        {
            Task? task = GetById(id);
            if (task == null || task.IsCompleted == completed) return null;

            task.IsCompleted = completed;

            string status = completed ? "Completed" : "pending";

            OnChangeStatus?.Invoke(task.Id, status);

            return task;
        }

    }
}
