namespace WorkflowApi.Entities
{
    public class AppTaskAudit
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public DateTime Date { get; set; }
        public string Changes { get; set; }

        public int EntityId { get; set; }
        public AppTask Entity { get; set; }
        public int UpdaterId { get; set; }
        public AppUser Updater { get; set; }
    }
}
