using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;
using TestProject.WebAPI.Contracts;
using TestProject.WebAPI.Data.Entities;

[assembly: InternalsVisibleTo("TestProject.Tests")]
namespace TestProject.WebAPI.Services
{
    class AccountService : IAccountService
    {
        private readonly IRepository _repository;

        public AccountService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            await _repository.InsertEntityAsync(account);
            await _repository.SaveChangesAsync();
        }

        public Task<Account> GetByUserIdAsync(long userId)
        {
            return _repository.GetEntityAsync<Account>(a=> a.UserId == userId);
        }

        public Task<List<Account>> GetAllAsync(int skip, int take)
        {
            if (take == 0)
                throw new ArgumentOutOfRangeException(nameof(take), "take must be a positive number");

            var specification = new Specification<Account> { Skip = skip, Take = take };
            return _repository.GetEntityListAsync(specification);
        }
    }
}