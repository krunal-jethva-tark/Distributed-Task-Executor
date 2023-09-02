using DTS.Models.Node;
using DTS.Models.Tasks;
using TaskExecutor.BusinessServices;

namespace TaskExecutor.Executor
{
    public class DTSEService
    {
        private readonly NodesBusinessService _nodesBusinessService;
        private readonly TaskQueueManager taskQueue;
        public DTSEService(NodesBusinessService nodesBusinessService, TaskQueueManager taskQueue)
        {
            _nodesBusinessService = nodesBusinessService;
            this.taskQueue = taskQueue;
        }

        public async Task ExecuteTasks()
        {
            // how to set true to this always Timer ??
            while (true)
            {
                DTSTask? task = taskQueue.DequeueTask();
                if (task != null)
                {
                    var node = _nodesBusinessService.GetAvailableNode();
                    if (node != null)
                    {
                        node.IsAvailable = false;
                        task.Status = DTS.Models.Utility.Enum.TaskStatus.Pending;
                        await ExecuteTaskOnWorker(task, node);
                        node.IsAvailable = true;
                    }
                }
            }
        }

        private async Task ExecuteTaskOnWorker(DTSTask task, Node node)
        {
            task = await node.ExecuteTask(task);
            if (task.Status.Equals(DTS.Models.Utility.Enum.TaskStatus.Failed))
            {
                taskQueue.EnqueueTask(task);
            }
        }
    }
}
