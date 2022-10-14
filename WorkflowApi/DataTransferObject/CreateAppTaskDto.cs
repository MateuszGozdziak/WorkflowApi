using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkflowApi.Entities;

namespace WorkflowApi.DataTransferObject
{
    public class CreateAppTaskDto
    {
        public int? Id { get; set; }//dodać do ts
        public double? Duration { get; set; }
        public DateTime EndDate { get; set; } = new DateTime();
        public double Progress { get; set; }
        public DateTime StartDate { get; set; } = new DateTime();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public int TeamId { get; set; }
        public int PriorityId { get; set; }
        public int StateId { get; set; }
        public string? Predecessor { get; set; }
        public string? rowColor { get; set; }
        //public List<Dictionary<string,object?>> AppTaskChanges { get; set; }
    }
}
