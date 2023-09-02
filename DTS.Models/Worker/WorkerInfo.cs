using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS.Models.Worker
{
    public class WorkerInfo
    {
        public WorkerInfo(string name, int port)
        {
            Name = name;
            Port = port;
            WorkDir = Path.Combine(Environment.CurrentDirectory, "work", Name);
        }

        public string Name { get; set; }
        public int Port { get; set; }
        public string WorkDir { get; set; }
    }
}
