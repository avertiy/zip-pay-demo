using FluentValidation;

namespace ZipPayDemo.Application.Users.Query.GetUsers
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Take).GreaterThan(0);
        }
    }
}
