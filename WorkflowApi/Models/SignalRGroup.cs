namespace WorkflowApi.Models
{
    public class SignalRGroup
    {
        public SignalRGroup()
        {
        }

        public SignalRGroup(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
