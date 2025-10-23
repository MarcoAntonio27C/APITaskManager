namespace TaskManager.API.Services
{
    public interface ITaskService
    {
        event ChangeStatusHandler OnChangeStatus;
        IEnumerable<Models.Task> GetTasks();
        Models.Task? GetById(Guid Id);
        Models.Task Create(Models.Task newTask);
        Models.Task? UpdateStatus(Guid id, bool completed);
        
    }
}
