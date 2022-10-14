namespace WorkflowApi.DataTransferObject
{
    public class AppTaskAuditDto
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public DateTime Date { get; set; }
        public string Changes { get; set; }
        public int EntityId { get; set; }
        public string UpdaterEmail { get; set; }
    }
}
