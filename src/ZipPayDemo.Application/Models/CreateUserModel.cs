using System.ComponentModel.DataAnnotations;

namespace TestProject.WebAPI.Models
{
    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public decimal MonthlySalary { get; set; }
        [Required]
        public decimal MonthlyExpenses { get; set; }
    }
}