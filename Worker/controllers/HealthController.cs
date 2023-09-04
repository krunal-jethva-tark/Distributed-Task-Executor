using Microsoft.AspNetCore.Mvc;

namespace Worker.controllers
{
    [Route("[controller]")]
    public class HealthController: ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
