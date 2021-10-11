using System.Collections.Generic;
using System.Threading.Tasks;
using ZipPayDemo.Domain.Entities;

namespace ZipPayDemo.Infrastructure.Contracts
{
    public interface IAccountService
    {
        Task CreateAccountAsync(Account account);
        Task<Account> GetByAccountIdAsync(long userId);
        Task<List<Account>> GetAllAsync(int skip, int take);
    }
}