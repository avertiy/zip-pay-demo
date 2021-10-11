using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ZipPayDemo.Application.Users.Command.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpenses { get; set; }
    }
}
