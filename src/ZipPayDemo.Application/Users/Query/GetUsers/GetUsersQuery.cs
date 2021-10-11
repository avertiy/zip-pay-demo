using MediatR;

namespace ZipPayDemo.Application.Users.Query.GetUsers
{
    public class GetUsersQuery : IRequest<GetUsersResponse>
    {
        public int Take { get; set; } = 20;
        public int Skip { get; set; }
    }
}
