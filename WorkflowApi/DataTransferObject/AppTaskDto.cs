namespace WorkflowApi.DataTransferObject
{
    public class AppTaskDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int TeamId { get; set; }
        public int PriorityId { get; set; }
        public int StateId { get; set; }
        public string? Predecessor { get; set; }
        public double Progress { get; set; }
        public int? ParentId { get; set; }
        public string? rowColor { get; set; }
        public string? Performer { get; set; }

    }
}
