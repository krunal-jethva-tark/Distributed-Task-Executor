namespace DTS.Model
{
    public class DTSTask
    {
        public DTSTask() { }

        public Guid Id { get; set; }
        public int Retries { get; set; }
        public string Status { get; set; }
    }
}