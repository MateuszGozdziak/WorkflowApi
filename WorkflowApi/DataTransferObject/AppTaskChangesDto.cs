namespace WorkflowApi.DataTransferObject
{
    public class AppTaskChangesDto
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public string Username { get; set; }
        public DateTime TimeStamp { get; set; }
        public int EntityId { get; set; }
    }
}