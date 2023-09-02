
namespace DTS.Models.Tasks
{
    public class DTSTask
    {
        public DTSTask(string taskType)
        {
            Id = new Guid(taskType);
            TaskType = taskType;
            Retries = 0;
        }
        public Guid Id { get; set; }
        public Utility.Enum.TaskStatus Status { get; set; }
        public string? TaskResult { get; set; }
        public string? TaskType { get; set; }
        public int Retries { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
