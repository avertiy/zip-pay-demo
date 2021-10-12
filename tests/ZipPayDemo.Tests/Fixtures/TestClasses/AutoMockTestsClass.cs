using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using AutoFixture;
using Newtonsoft.Json;
using Xunit;

namespace ZipPayDemo.Tests.Fixtures.TestClasses
{
    [ExcludeFromCodeCoverage]
    public abstract class AutoMockTestsClass<TTarget> : AutoMockBaseClass<TTarget>, IClassFixture<CommonTestsFixture>
    {
        private readonly CommonTestsFixture _testsFixture;
        protected Fixture Fixture => _testsFixture.Fixture;
        protected AutoMockTestsClass(CommonTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        protected HttpResponseMessage OkResponse(object content)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = content != null ? new StringContent(JsonConvert.SerializeObject(content)) : null
            };
        }

        protected HttpResponseMessage ErrorResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };
        }
    }
}
