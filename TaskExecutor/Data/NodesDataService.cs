
using DTS.Models.Node;

namespace TaskExecutor.Data
{
    public class NodesDataService
    {
        private readonly List<Node> nodes;

        public NodesDataService()
        {
            nodes = new List<Node>();
        }

        public Node AddNode(Node node)
        {
            if (nodes.Any(x => x.Name == node.Name))
            {
                throw new InvalidOperationException($"Node with name: {node.Name} already exists");
            }
            nodes.Add(node);
            return node;
        }

        public Node RemoveNode(string name)
        {
            Node? node = nodes.FirstOrDefault(x => x.Name == name);
            if (node != null)
            {
                nodes.Remove(node);
                return node;
            }
            // handle error handing here
            throw new InvalidOperationException($"Node with Name {name} does not exists");
        }

        public List<Node> GetAll()
        {
            return nodes;
        }

        internal Node? GetAvailableNode()
        {
            return nodes.FirstOrDefault(node => node.IsAvailable);
        }
    }
}
