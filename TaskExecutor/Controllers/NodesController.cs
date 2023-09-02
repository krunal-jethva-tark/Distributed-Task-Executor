using DTS.Models.Node;
using Microsoft.AspNetCore.Mvc;
using TaskExecutor.BusinessServices;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodesController : ControllerBase
    {
        private readonly NodesBusinessService _nodesBusinessService;
        public NodesController(NodesBusinessService nodesBusinessService)
        {
            _nodesBusinessService = nodesBusinessService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterNode([FromBody] NodeRegistrationRequest node)
        {
            _nodesBusinessService.RegisterNode(node);
            return Ok();
        }
        
        [HttpDelete]
        [Route("unregister/{name}")]
        public IActionResult RegisterNode(string name)
        {
            _nodesBusinessService.UnregisterNode(name);
            return Ok();
        }
    }
}
