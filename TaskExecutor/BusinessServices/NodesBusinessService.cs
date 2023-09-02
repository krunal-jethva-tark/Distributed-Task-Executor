using DTS.Models.Node;
using TaskExecutor.Data;

namespace TaskExecutor.BusinessServices
{
    public class NodesBusinessService
    {
        private readonly NodesDataService _dataService;
        public NodesBusinessService(NodesDataService nodesDataService)
        {
            _dataService = nodesDataService;
        }

        public void RegisterNode(NodeRegistrationRequest node)
        {
            _dataService.AddNode(new Node(node.Name, node.Address));
        }

        public void UnregisterNode(string name)
        {
            _dataService.RemoveNode(name);
        }

        public List<Node> GetNodes()
        {
            return _dataService.GetAll();
        }

        public Node? GetAvailableNode()
        {
            return _dataService.GetAvailableNode();
        }
    }
}
