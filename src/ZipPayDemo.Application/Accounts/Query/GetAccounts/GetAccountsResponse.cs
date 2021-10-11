using System.Collections.Generic;
using TestProject.WebAPI.Models;

namespace ZipPayDemo.Application.Accounts.Query.GetAccounts
{
    public class GetAccountsResponse
    {
        public IList<AccountModel> Accounts { get; set; }
    }
}
