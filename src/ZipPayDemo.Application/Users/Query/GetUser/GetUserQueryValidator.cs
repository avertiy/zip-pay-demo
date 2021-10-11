using FluentValidation;

namespace ZipPayDemo.Application.Users.Query.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0);
        }
    }
}
