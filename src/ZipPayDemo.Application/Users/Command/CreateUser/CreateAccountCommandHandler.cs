using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ZipPayDemo.Application.Utilities.Extensions;
using ZipPayDemo.Domain.Entities;
using ZipPayDemo.Infrastructure.Contracts;

namespace ZipPayDemo.Application.Users.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(
            ILogger<CreateUserCommandHandler> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(CreateUserCommand), requestJson);

                var user = new User
                {
                    Email = request.Email,
                    MonthlySalary = request.MonthlySalary,
                    MonthlyExpenses = request.MonthlyExpenses,
                    Name = request.Name
                };
                await _userService.CreateUserAsync(user);
                return new CreateUserResponse() { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(CreateUserCommand), requestJson);
                throw;
            }
        }
    }
}
