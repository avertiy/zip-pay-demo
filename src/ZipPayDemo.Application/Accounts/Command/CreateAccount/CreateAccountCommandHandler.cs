using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ZipPayDemo.Application.Utilities.Extensions;
using ZipPayDemo.Domain.Entities;
using ZipPayDemo.Infrastructure.Contracts;

namespace ZipPayDemo.Application.Accounts.Command.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<CreateAccountCommandHandler> _logger;
        public CreateAccountCommandHandler(
            ILogger<CreateAccountCommandHandler> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(CreateAccountCommand), requestJson);

                var account = new Account { UserId = request.UserId };
                await _accountService.CreateAccountAsync(account);
                return new CreateAccountResponse() { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(CreateAccountCommand), requestJson);
                throw;
            }
        }
    }
}
