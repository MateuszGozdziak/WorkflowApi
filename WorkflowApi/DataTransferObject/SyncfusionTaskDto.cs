namespace WorkflowApi.DataTransferObject
{
    public class SyncfusionTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Duration { get; set; }
        public int Progress { get; set; }
        public string Description { get; set; }
        //public int PriorityId { get; set; }
        public int TeamId { get; set; }
        //public int Child { get; set; }
        public string Predecessor { get; set; }
    }
}
