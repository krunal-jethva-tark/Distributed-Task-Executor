using DTS.Models.Tasks;
using TaskExecutor.Executor;

namespace TaskExecutor.BusinessServices
{
    public class TaskExecutorBusinessService
    {
        private readonly TaskQueueManager taskQueueManager;

        public TaskExecutorBusinessService(TaskQueueManager taskQueueManager)
        {
            this.taskQueueManager = taskQueueManager;
        }

        public void ExecuteTask(string taskName)
        {
            var task = new DTSTask(taskName);
            taskQueueManager.EnqueueTask(task);
        }
    }
}
