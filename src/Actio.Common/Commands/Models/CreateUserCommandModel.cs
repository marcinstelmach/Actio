namespace Actio.Common.Commands.Models
{
    public class CreateUserCommandModel : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
