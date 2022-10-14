using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkflowApi.Entities;

namespace WorkflowApi.Models
{
    public class AppTaskDependencies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AppTaskOneId { get; set; }
        public int AppTaskTwoId { get; set; }
        public virtual AppTask AppTaskStart { get; set; }
        public virtual AppTask AppTaskEnd { get; set; }
    }
}
