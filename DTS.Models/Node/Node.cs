using DTS.Models.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DTS.Models.Node
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsAvailable { get; set; }
        public List<DTSTask> Tasks { get; set; }

        public Node(string name, string address)
        {
            Name = name;
            Address = address;
            Tasks = new List<DTSTask>();
        }


        public async Task<DTSTask> ExecuteTask(DTSTask task)
        {
            //ADD response here
            var client = new HttpClient();
            var response = client.PostAsJsonAsync($"{Address}api/task", task);
            var result = response.Wait(20 * 1000);
            if (result)
            {
                if (response?.Result?.Content != null)
                {
                    task = JsonConvert.DeserializeObject<DTSTask>(response.Result.Content.ToString()) ?? task;
                } else
                {
                    task.Status = Utility.Enum.TaskStatus.Failed;
                }
            } else
            {
                task.Status = Utility.Enum.TaskStatus.Failed;
            }
            return task;
        }


    }
}
