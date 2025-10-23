namespace TaskManager.API.Models
{
    public class EmailServerOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string ApiKey { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;

    }
}
