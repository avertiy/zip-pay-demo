using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TestProject.WebAPI.Models;
using ZipPayDemo.Application.Utilities.Extensions;
using ZipPayDemo.Infrastructure.Contracts;

namespace ZipPayDemo.Application.Accounts.Query.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountQueryHandler> _logger;
        public GetAccountQueryHandler(
            ILogger<GetAccountQueryHandler> logger, IAccountService accountService, IMapper mapper)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<GetAccountResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(GetAccountQuery), requestJson);

                var account = await _accountService.GetByAccountIdAsync(request.UserId);
                return new GetAccountResponse()
                {
                    Account = _mapper.Map<AccountModel>(account)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(GetAccountQuery), requestJson);
                throw;
            }
        }
    }
}
