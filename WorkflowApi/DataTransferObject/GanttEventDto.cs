namespace WorkflowApi.DataTransferObject
{
    public class GanttEventDto
    {
        public int? Id { get; set; }
        public DateTime Day { get; set; }
        public string Label { get; set; }
        public string CssClass { get; set; }
        public int TeamId { get; set; }

    }
}
