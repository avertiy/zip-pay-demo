using System.Net.Http;
using Xunit;
using ZipPayDemo.Tests.IntegrationTests.Setup;

namespace ZipPayDemo.Tests.IntegrationTests.Controllers
{
    public class AccountControllerIntegrationTests : IClassFixture<IntegrationTestsFixture>
    {
        private readonly HttpClient _client;

        public AccountControllerIntegrationTests(IntegrationTestsFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        //[Fact]
        //public async Task Can_Get_Accounts()
        //{
        //    var httpResponse = await _client.GetAsync("/api/accounts");

        //    // Must be successful.
        //    httpResponse.EnsureSuccessStatusCode();

        //    // Deserialize and examine results.

        //    var accounts = await httpResponse.ReadBody<IEnumerable<AccountModel>>();


        //    Assert.NotNull(accounts);
        //    Assert.Contains(accounts, a=> a.UserId == 1);
        //    Assert.Contains(accounts, a => a.UserId == 2);
        //}
        
        //[Fact]
        //public async Task Can_Get_Account_ByUserId()
        //{
        //    var httpResponse = await _client.GetAsync("/api/accounts/2");

        //    httpResponse.EnsureSuccessStatusCode();
        //    var account = await httpResponse.ReadBody<AccountModel>();

        //    Assert.NotNull(account);
        //    Assert.Equal(2, account.UserId);
        //}

        //[Fact]
        //public async Task Can_Create_Account()
        //{
        //    var httpResponse = await _client.PostAsync("/api/accounts/",
        //        new CreateAccountCommand(){ UserId = 4 }, 
        //        new JsonMediaTypeFormatter());
            
        //    Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        //}
    }
}