using DTS.Models.Tasks;
using DTS.Models.Tasks.TaskType;

namespace Worker.BusinessServices
{
    public class TaskBusinessService
    {
        private readonly ITaskType taskType;
        public TaskBusinessService(ITaskType taskType)
        {
            this.taskType = taskType;
        }


        public async Task<DTSTask> ExecuteTaskAsync(DTSTask task)
        {
            // resolve task type and execute the task
            var taskResult = await taskType.ExecuteTask(task);
            return taskResult;
        }
    }
}
