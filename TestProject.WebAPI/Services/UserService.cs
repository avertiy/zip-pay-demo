using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;
using TestProject.WebAPI.Contracts;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.Data.Entities;

[assembly: InternalsVisibleTo("TestProject.Tests")]
namespace TestProject.WebAPI.Services
{
    class UserService : IUserService
    {
        private readonly IRepository _repository;
        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<User> GetByIdAsync(long id)
        {
            return _repository.GetEntityByIdAsync<User>(id);
        }

        public async Task Create(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            await _repository.InsertEntityAsync(user);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync(int skip, int take)
        {
            if(take == 0)
                throw new ArgumentOutOfRangeException(nameof(take), "take must be a positive number");

            var specification = new Specification<User> {Skip = skip, Take = take};
            
            var users = await _repository.GetEntityListAsync(specification);
            return users;
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _repository.GetEntityAsync<User>(u => u.Email == email);
        }
    }
}