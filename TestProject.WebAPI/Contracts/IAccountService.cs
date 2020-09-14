using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Data.Entities;

namespace TestProject.WebAPI.Contracts
{
    public interface IAccountService
    {
        Task CreateAccount(Account account);
        Task<Account> GetByUserIdAsync(long userId);
        Task<List<Account>> GetAllAsync(int skip, int take);
    }
}