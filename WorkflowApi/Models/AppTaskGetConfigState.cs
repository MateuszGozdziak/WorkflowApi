namespace WorkflowApi.Models
{
    public class AppTaskGetConfigState
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string? sortDirection { get; set; }
        public string? sortName { get; set; }
    }
}
