namespace Actio.Common.Commands.Models
{
    public class AuthenticateUserCommandModel : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
