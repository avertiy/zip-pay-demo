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

namespace ZipPayDemo.Application.Users.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersQueryHandler> _logger;
        public GetUsersQueryHandler(
            ILogger<GetUsersQueryHandler> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(GetUsersQuery), requestJson);

                var users = await _userService.GetAllAsync(request.Skip, request.Take);
                return new GetUsersResponse()
                {
                    Users = _mapper.Map<IList<UserModel>>(users)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(GetUsersQuery), requestJson);
                throw;
            }
        }
    }
}
