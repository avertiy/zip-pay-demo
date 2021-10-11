using MediatR;

namespace ZipPayDemo.Application.Accounts.Query.GetAccount
{
    public class GetAccountQuery : IRequest<GetAccountResponse>
    {
        public long UserId { get; set; }
    }
}
