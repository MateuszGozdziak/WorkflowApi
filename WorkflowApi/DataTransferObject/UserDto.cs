using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.DataTransferObject
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
