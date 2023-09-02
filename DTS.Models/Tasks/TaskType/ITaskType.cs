
namespace DTS.Models.Tasks.TaskType
{
    public interface ITaskType
    {
        Task<DTSTask> ExecuteTask(DTSTask task);
    }
}
