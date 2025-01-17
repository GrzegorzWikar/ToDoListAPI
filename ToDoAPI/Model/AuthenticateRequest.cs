using System.ComponentModel;

namespace ToDoAPI.Model
{
    public class AuthenticateRequest
    {
        [DefaultValue("System")]
        public required string Username { get; set; }
        [DefaultValue("System")]
        public required string Password { get; set; }
    }
}
