namespace TaskManager.API.Models
{
    public class Message<TData> where TData : class
    {
        public string Addressee { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public TData Data { get; set; } = null!;
    }
}
