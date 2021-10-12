using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;
using ZipPayDemo.Application.Utilities.Extensions;
using ZipPayDemo.Tests.IntegrationTests.Setup;
using ZipPayDemo.Tests.Utilities.Extensions;

namespace ZipPayDemo.Tests.Fixtures.TestClasses
{
    [ExcludeFromCodeCoverage]
    public abstract class IntegrationTestsClass : IClassFixture<ApiFactory>
    {
        protected HttpClient Client { get; }
        public const string TestingHeader = "X-Integration-Testing";
        public const string TestingHeaderValue = "test-header";

        protected IntegrationTestsClass(ApiFactory factory)
        {
            Client = factory.CreateClient();
            Client.DefaultRequestHeaders.Add(TestingHeader, TestingHeaderValue);
        }

        protected void AddHeader(string name, string value)
        {
            Client.DefaultRequestHeaders.Add(name, value);
        }

        protected void ResetRequestHeaders()
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add(TestingHeader, TestingHeaderValue);
        }

        protected async Task<T> Test200OkResult<T>(TestRequest request, Action<T> assert = null)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            var result = await response.Should().BeOkResult<T>();
            result.Should().NotBeNull();
            assert?.Invoke(result);
            return result;
        }

        protected async Task Test200OkResult(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeOkResult();
        }

        protected async Task<T> Test201CreatedResult<T>(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            var result = await response.Should().BeCreatedResult<T>();
            result.Should().NotBeNull();
            return result;
        }

        protected async Task Test204NoContent(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeNoContent();
        }

        protected async Task Test404NotFound(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeNotFound();
        }

        protected async Task Test403Forbidden(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeForbidden();
        }

        protected async Task Test400BadRequest(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeBadRequest();
        }

        protected async Task Test401Unauthorized(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeUnauthorized();
        }

        protected async Task Test422UnprocessableEntity(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeUnprocessableEntity();
        }

        protected async Task Test424FailedDependency(TestRequest request)
        {
            // Act
            using var response = await GetHttpResponseMessageAsync(request);

            // Assert
            await response.Should().BeFailedDependency();
        }

        protected StringContent GetStringContent(object data)
        {
            return new StringContent(data.ToJsonString(), Encoding.UTF8, "application/json");
        }

        private async Task<HttpResponseMessage> GetHttpResponseMessageAsync(Uri uri, string httpMethod, StringContent stringContent = null)
        {
            if (HttpMethods.IsGet(httpMethod))
            {
                return await Client.GetAsync(uri);
            }

            if (HttpMethods.IsPost(httpMethod))
            {
                return await Client.PostAsync(uri, stringContent);
            }

            if (HttpMethods.IsPut(httpMethod))
            {
                return await Client.PutAsync(uri, stringContent);
            }

            throw new NotImplementedException();
        }

        private async Task<HttpResponseMessage> GetHttpResponseMessageAsync(TestRequest request)
        {
            if (HttpMethods.IsGet(request.HttpMethod))
            {
                return await Client.GetAsync(request.Uri);
            }

            if (HttpMethods.IsPost(request.HttpMethod))
            {
                return await Client.PostAsync(request.Uri, request.Content);
            }

            if (HttpMethods.IsPut(request.HttpMethod))
            {
                return await Client.PutAsync(request.Uri, request.Content);
            }

            throw new NotImplementedException();
        }
    }

    public class TestRequest
    {
        public string Url { get; set; }
        public string HttpMethod { get; set; } = HttpMethods.Get;
        public StringContent Content { get; set; }
        public Uri Uri => new Uri(Url, UriKind.Relative);

        public TestRequest()
        {
        }

        public TestRequest(string url, object data, string method = "POST")
        {
            Url = url;
            Content = new StringContent(data.ToJsonString(), Encoding.UTF8, "application/json");
            HttpMethod = method;
        }

        public static implicit operator TestRequest(string url)
        {
            return new TestRequest() { Url = url };
        }
    }
}