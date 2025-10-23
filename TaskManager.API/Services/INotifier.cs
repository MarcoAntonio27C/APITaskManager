using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public interface INotifier
    {
        bool Send(Message<object> message);
    }
}
