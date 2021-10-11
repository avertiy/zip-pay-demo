using FluentValidation;

namespace ZipPayDemo.Application.Accounts.Query.GetAccounts
{
    public class GetAccountsQueryValidator : AbstractValidator<GetAccountsQuery>
    {
        public GetAccountsQueryValidator()
        {
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Take).GreaterThan(0);
        }
    }
}
