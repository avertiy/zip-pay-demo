using Microsoft.EntityFrameworkCore.Internal;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.Data.Entities;

namespace TestProject.WebAPI.SeedData
{
    public static class SeedData
    {
        public static void PopulateTestData(DataContext context)
        {
            AddUsers(context);
            AddAccounts(context);
        }

        private static void AddAccounts(DataContext context)
        {
            if (!context.Accounts.Any())
            {
                context.Accounts.Add(new Account() { Id = 1, UserId = 1 });
                context.Accounts.Add(new Account() { Id = 2, UserId = 2 });
                context.SaveChanges();
            }
        }

        private static void AddUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User() { Id = 1, Name = "user1", Email = "user1@mail.com", MonthlySalary = 2500, MonthlyExpenses = 500 });
                context.Users.Add(new User() { Id = 2, Name = "user2", Email = "user2@mail.com", MonthlySalary = 2500, MonthlyExpenses = 1000 });
                context.Users.Add(new User() { Id = 3, Name = "user3", Email = "user3@mail.com", MonthlySalary = 2000, MonthlyExpenses = 1500 });
                context.Users.Add(new User() { Id = 4, Name = "user3", Email = "user3@mail.com", MonthlySalary = 2500, MonthlyExpenses = 1000 });
                context.SaveChanges();
            }
        }
    }
}