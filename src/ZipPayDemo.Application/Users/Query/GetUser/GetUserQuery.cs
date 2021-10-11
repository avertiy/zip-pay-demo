using MediatR;

namespace ZipPayDemo.Application.Users.Query.GetUser
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public long UserId { get; set; }
    }
}
