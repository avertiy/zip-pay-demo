using MediatR;

namespace ZipPayDemo.Application.Accounts.Command.CreateAccount
{
    public class CreateAccountCommand : IRequest<CreateAccountResponse>
    {
        public long UserId { get; set; }
    }
}
