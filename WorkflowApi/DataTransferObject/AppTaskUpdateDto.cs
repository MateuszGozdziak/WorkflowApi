using WorkflowApi.Entities;

namespace WorkflowApi.DataTransferObject
{
    public class AppTaskUpdateDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public int StateId { get; set; }
        public int TeamId { get; set; }
        public ICollection<AppTaskChanges> AppTaskChanges { get; set; }

    }
}
