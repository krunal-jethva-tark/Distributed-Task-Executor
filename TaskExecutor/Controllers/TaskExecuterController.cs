using Microsoft.AspNetCore.Mvc;
using TaskExecutor.BusinessServices;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskExecuterController: ControllerBase
    {
        private readonly TaskExecutorBusinessService _taskExecutorBusinessService;

        public TaskExecuterController(TaskExecutorBusinessService taskExecutorBusinessService)
        {
            _taskExecutorBusinessService = taskExecutorBusinessService;
        }
        [HttpPost]
        [Route("{taskName")]
        public IActionResult ExecuteTask(string taskName)
        {
            _taskExecutorBusinessService.ExecuteTask(taskName);
            return Ok();
        }
    }
}
