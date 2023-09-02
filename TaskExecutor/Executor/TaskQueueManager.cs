using DTS.Models.Tasks;

namespace TaskExecutor.Executor
{
    public class TaskQueueManager
    {
        private readonly static Queue<DTSTask> queue = new();
        public void EnqueueTask(DTSTask task)
        {
            queue.Enqueue(task);
        }

        public DTSTask? DequeueTask()
        {
            if (queue.Count > 0)
            {
                return queue.Dequeue();
            }
            return null;
        }
    }
}
