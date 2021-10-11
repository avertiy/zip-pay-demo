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

namespace ZipPayDemo.Application.Accounts.Query.GetAccounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, GetAccountsResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAccountsQueryHandler> _logger;
        public GetAccountsQueryHandler(
            ILogger<GetAccountsQueryHandler> logger, IAccountService accountService, IMapper mapper)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<GetAccountsResponse> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(GetAccountsQuery), requestJson);

                var users = await _accountService.GetAllAsync(request.Skip, request.Take);
                return new GetAccountsResponse()
                {
                    Accounts = _mapper.Map<IList<AccountModel>>(users)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(GetAccountsQuery), requestJson);
                throw;
            }
        }
    }
}
