using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ZipPayDemo.Infrastructure.Contracts;

namespace ZipPayDemo.Application.Accounts.Command.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        private readonly IUserService _userService;
        public CreateAccountCommandValidator(IUserService userService)
        {
            _userService = userService;
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.UserId).GreaterThan(0).MustAsync(ValidateUserAsync);
            

            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0);
        }

        private async Task<bool> ValidateUserAsync(CreateAccountCommand cmd, long userId, 
            ValidationContext<CreateAccountCommand> ctx, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                ctx.AddFailure("UserId", "User does not exist");
                return false;
            }

            if (user.MonthlySalary - user.MonthlyExpenses < 1000)
            {
                ctx.AddFailure("UserId", "User does not meet account requirements");
                return false;
            }

            return true;
        }
    }
}
