using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Data.Entities;

namespace TestProject.WebAPI.Contracts
{
    public interface IUserService
    {
        Task Create(User user);
        Task<User> GetByIdAsync(long id);
        Task<List<User>> GetAllAsync(int skip, int take);
        Task<User> GetUserByEmailAsync(string userEmail);
    }
}