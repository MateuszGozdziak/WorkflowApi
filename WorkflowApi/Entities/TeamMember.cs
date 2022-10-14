using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Entities
{
    public class TeamMember
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
