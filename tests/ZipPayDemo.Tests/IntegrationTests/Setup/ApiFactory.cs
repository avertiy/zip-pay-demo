using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace ZipPayDemo.Tests.IntegrationTests.Setup
{
    public class ApiFactory : WebApplicationFactory<TestStartup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException($"{nameof(builder)} should be specified");
            }

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.UseConfiguration(configuration)
                .UseKestrel(options =>
                {
                    options.AllowSynchronousIO = true;
                })
                .UseSolutionRelativeContentRoot("")
                .UseStartup<TestStartup>();
        }
    }
}
