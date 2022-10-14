using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Entities
{
    public class AppUser : IdentityUser<int>
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<UserInvited> UsersWhoInvite { get; set; }
        public ICollection<UserInvited> InvitedByUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<AppTask> UpdatedAppTasks { get; set; }
        public ICollection<AppTaskAudit> AppTaskAudit { get; set; }
        


    }
}