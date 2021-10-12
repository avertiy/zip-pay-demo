using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ZipPayDemo.Application.Users.Query.GetUser
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        [FromRoute]
        public long UserId { get; set; }
    }
}
