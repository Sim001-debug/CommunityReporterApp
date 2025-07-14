namespace CommunityReporterApp.Models
{
    public class LoginUserRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
