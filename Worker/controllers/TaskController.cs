using DTS.Model;
using Microsoft.AspNetCore.Mvc;
using Worker.BusinessServices;

namespace Worker.controllers
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

        [HttpGet]
        public ActionResult<DTSTask> Get(Guid id)
        {
            var task = taskBusinessService.GetTask(id);
            return Ok(task);
        }
            
        [HttpPost]
        public ActionResult Post(DTSTask task)
        {
            taskBusinessService.ExecuteTaskAsync(task);
            return Ok(task);
        }
    }
}
