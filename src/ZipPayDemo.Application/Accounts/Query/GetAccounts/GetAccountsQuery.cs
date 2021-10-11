using MediatR;

namespace ZipPayDemo.Application.Accounts.Query.GetAccounts
{
    public class GetAccountsQuery : IRequest<GetAccountsResponse>
    {
        public int Take { get; set; } = 20;
        public int Skip { get; set; }
    }
}
