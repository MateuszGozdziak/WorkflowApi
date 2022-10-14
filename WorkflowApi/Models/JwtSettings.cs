namespace WorkflowApi.Models
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }
        public int jwtExpire { get; set; }
        public string jwtIssuer { get; set; }
    }
}
