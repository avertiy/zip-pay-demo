using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ZipPayDemo.Infrastructure.Contracts;

namespace ZipPayDemo.Application.Users.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;
        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MustAsync(ValidateEmailAsync);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.MonthlySalary).GreaterThan(0);
            RuleFor(x => x.MonthlyExpenses).GreaterThanOrEqualTo(0);
        }

        private async Task<bool> ValidateEmailAsync(CreateUserCommand cmd, string email,
            ValidationContext<CreateUserCommand> ctx, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                ctx.AddFailure("Email", $"User with such email [{email}] already exists");
                return false;
            }

            return true;
        }
    }
}
