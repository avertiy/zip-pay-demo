using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TestProject.WebAPI.Models;
using ZipPayDemo.Application.Utilities.Extensions;
using ZipPayDemo.Infrastructure.Contracts;

namespace ZipPayDemo.Application.Users.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserQueryHandler> _logger;
        public GetUserQueryHandler(
            ILogger<GetUserQueryHandler> logger, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(GetUserQuery), requestJson);

                var account = await _userService.GetByIdAsync(request.UserId);
                return new GetUserResponse()
                {
                    User = _mapper.Map<UserModel>(account)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(GetUserQuery), requestJson);
                throw;
            }
        }
    }
}
