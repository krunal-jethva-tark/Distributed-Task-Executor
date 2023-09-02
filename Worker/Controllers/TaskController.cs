using DTS.Models.Tasks;
using Microsoft.AspNetCore.Mvc;
using Worker.BusinessServices;

namespace Worker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController: ControllerBase
    {
        private readonly TaskBusinessService taskBusinessService;
        public TaskController(TaskBusinessService taskBusinessService)
        {
            this.taskBusinessService = taskBusinessService;
        }

        [HttpPost]
        public ActionResult AddTask(DTSTask task)
        {
            taskBusinessService.ExecuteTaskAsync(task);
            return Ok(task);
        }
    }
}
