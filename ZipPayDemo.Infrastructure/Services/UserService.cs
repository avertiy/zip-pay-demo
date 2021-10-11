using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;
using ZipPayDemo.Domain.Entities;
using ZipPayDemo.Infrastructure.Contracts;

namespace ZipPayDemo.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<User> GetByIdAsync(long id)
        {
            return _repository.GetByIdAsync<User>(id);
        }

        public async Task CreateUserAsync(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            await _repository.InsertAsync(user);
        }

        public async Task<List<User>> GetAllAsync(int skip, int take)
        {
            if(take == 0)
                throw new ArgumentOutOfRangeException(nameof(take), "take must be a positive number");

            var specification = new Specification<User> {Skip = skip, Take = take};
            
            var users = await _repository.GetListAsync(specification);
            return users;
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _repository.GetAsync<User>(u => u.Email == email);
        }
    }
}