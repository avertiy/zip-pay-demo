using FluentValidation;

namespace ZipPayDemo.Application.Accounts.Query.GetAccount
{
    public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0);
        }
    }
}
