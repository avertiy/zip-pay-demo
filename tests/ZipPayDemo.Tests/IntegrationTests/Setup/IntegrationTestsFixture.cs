using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZipPayDemo.Api;
using ZipPayDemo.Api.Data;
using ZipPayDemo.Api.SeedData;

namespace ZipPayDemo.Tests.IntegrationTests.Setup
{
    public class TestStartup : Startup
    {
        //database root is required for in-memory database data persistence
        private readonly InMemoryDatabaseRoot _databaseRoot = new InMemoryDatabaseRoot();
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureDatabaseServices(IServiceCollection services)
        {
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DataContext>((sp, options) =>
                {
                    options.UseInMemoryDatabase("InMemory", _databaseRoot).UseInternalServiceProvider(sp);
                });
        }

        protected override void EnsureDatabaseCreated(DataContext context)
        {
            context.Database.EnsureCreated();
            SeedData.PopulateTestData(context);
        }
    }

    public class IntegrationTestsFixture : WebApplicationFactory<TestStartup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder().UseStartup<TestStartup>();
    } 
}
