using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;
using ZipPayDemo.Domain.Entities;
using ZipPayDemo.Infrastructure.Contracts;

[assembly: InternalsVisibleTo("ZipPayDemo.Tests")]
namespace ZipPayDemo.Infrastructure.Services
{
    class AccountService : IAccountService
    {
        private readonly IRepository _repository;

        public AccountService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAccountAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            await _repository.InsertAsync(account);
        }

        public Task<Account> GetByAccountIdAsync(long userId)
        {
            return _repository.GetAsync<Account>(a=> a.UserId == userId);
        }

        public Task<List<Account>> GetAllAsync(int skip, int take)
        {
            if (take == 0)
                throw new ArgumentOutOfRangeException(nameof(take), "take must be a positive number");

            var specification = new Specification<Account> { Skip = skip, Take = take };
            return _repository.GetListAsync(specification);
        }
    }
}