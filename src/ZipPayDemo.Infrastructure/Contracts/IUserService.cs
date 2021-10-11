using System.Collections.Generic;
using System.Threading.Tasks;
using ZipPayDemo.Domain.Entities;

namespace ZipPayDemo.Infrastructure.Contracts
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User> GetByIdAsync(long id);
        Task<List<User>> GetAllAsync(int skip, int take);
        Task<User> GetUserByEmailAsync(string userEmail);
    }
}