namespace TestProject.WebAPI.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal MonthlyExpenses { get; set; }
    }
}