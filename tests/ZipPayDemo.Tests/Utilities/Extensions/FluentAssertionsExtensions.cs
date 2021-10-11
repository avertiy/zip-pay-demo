using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Primitives;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZipPayDemo.Tests.Utilities.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class FluentAssertionsExtensions
    {
        public static async Task<T> BeOkResult<T>(this ObjectAssertions actualValue)
        {
            if (actualValue.Subject is HttpResponseMessage responseMessage)
            {
                string content = null;
                if (responseMessage.Content != null)
                {
                    content = await responseMessage.Content.ReadAsStringAsync();
                }

                responseMessage.IsSuccessStatusCode.Should().BeTrue($"{responseMessage.ReasonPhrase} {content}");

                if (string.IsNullOrEmpty(content))
                {
                    return default;
                }

                return JsonConvert.DeserializeObject<T>(content);
            }

            actualValue.BeOfType<OkObjectResult>();
            var res = (OkObjectResult)actualValue.Subject;
            res.Value.Should().BeOfType<T>();
            return (T)res.Value;
        }

        public static async Task<T> BeOkResult<T>(this ObjectAssertions actualValue, Action<T> assert) where T : class
        {
            if (actualValue.Subject is HttpResponseMessage responseMessage)
            {
                responseMessage.EnsureSuccessStatusCode();
                var content = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            actualValue.BeOfType<OkObjectResult>();
            var res = (OkObjectResult)actualValue.Subject;
            res.StatusCode.Should().Be((int)HttpStatusCode.OK);
            res.Value.Should().BeOfType<T>();
            T obj = (T)res.Value;

            if (assert != null)
            {
                obj.Should().NotBeNull();
                assert(obj);
            }

            return obj;
        }

        public static async Task<T> BeOkObjectResult<T>(this ObjectAssertions actualValue, HttpStatusCode statusCode,
            Action<T> assert = null) where T : class
        {
            if (actualValue.Subject is HttpResponseMessage responseMessage)
            {
                responseMessage.EnsureSuccessStatusCode();
                var content = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            actualValue.BeOfType<OkObjectResult>();
            var res = (OkObjectResult)actualValue.Subject;
            res.StatusCode.Should().Be((int) statusCode);
            res.Value.Should().BeOfType<T>();
            T obj = (T)res.Value;

            if (assert != null)
            {
                obj.Should().NotBeNull();
                assert(obj);
            }

            return obj;
        }

        public static async Task BeOkResult(this ObjectAssertions actualValue)
        {
            string message = string.Empty;
            if (actualValue.Subject is HttpResponseMessage responseMessage)
            {
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }

                message = await responseMessage.Content.ReadAsStringAsync();
            }

            actualValue.BeOfType<OkResult>(message);
        }

        public static async Task<T> BeObjectResult<T>(this ObjectAssertions actualValue, HttpStatusCode statusCode,
            Action<T> assert = null)
        {
            if (actualValue.Subject is HttpResponseMessage responseMessage)
            {
                string content = null;
                if (responseMessage.Content != null)
                {
                    content = await responseMessage.Content.ReadAsStringAsync();
                }

                responseMessage.IsSuccessStatusCode.Should().BeTrue($"{responseMessage.ReasonPhrase} {content}");

                if (string.IsNullOrEmpty(content))
                {
                    return default;
                }

                return JsonConvert.DeserializeObject<T>(content);
            }

            var objResult = actualValue.BeOfType<ObjectResult>().Subject;
            objResult.StatusCode.Should().Be((int)statusCode);
            objResult.Value.Should().BeOfType<T>();
            var obj = (T)objResult.Value;

            if (assert != null)
            {
                obj.Should().NotBeNull();
                assert(obj);
            }

            return obj;
        }

        public static async Task<T> BeCreatedResult<T>(this ObjectAssertions actualValue)
        {
            if (actualValue.Subject is HttpResponseMessage responseMessage)
            {
                string content = null;
                if (responseMessage.Content != null)
                {
                    content = await responseMessage.Content.ReadAsStringAsync();
                }

                responseMessage.IsSuccessStatusCode.Should().BeTrue($"{responseMessage.ReasonPhrase} {content}");

                if (string.IsNullOrEmpty(content))
                {
                    return default;
                }

                return JsonConvert.DeserializeObject<T>(content);
            }

            actualValue.BeOfType<CreatedResult>();
            var res = (CreatedResult)actualValue.Subject;
            res.Value.Should().BeOfType<T>();
            return (T)res.Value;
        }

        public static async Task<string> BeInternalServerError(this ObjectAssertions actualValue, string expectedContent = null)
        {
            actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
            var message = (HttpResponseMessage)actualValue.Subject;
            var content = await message.Content.ReadAsStringAsync();
            message.StatusCode.Should().Be(HttpStatusCode.InternalServerError, content);

            if (expectedContent != null)
            {
                content.Should().Contain(expectedContent);
            }

            return content;
        }

        public static async Task BeNoContent(this ObjectAssertions actualValue)
        {
            if (actualValue.Subject is NoContentResult noContentResult)
            {
                noContentResult.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            }
            else
            {
                actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
                var message = (HttpResponseMessage)actualValue.Subject;
                var content = await message.Content.ReadAsStringAsync();
                message.StatusCode.Should().Be(HttpStatusCode.NoContent, content);
            }
        }

        public static async Task BeUnauthorized(this ObjectAssertions actualValue)
        {
            actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
            var message = (HttpResponseMessage)actualValue.Subject;
            string content = null;
            if (message.Content != null)
            {
                content = await message.Content.ReadAsStringAsync();
            }

            message.StatusCode.Should().Be(HttpStatusCode.Unauthorized, $"{message.ReasonPhrase} {content}");
        }

        public static async Task BeForbidden(this ObjectAssertions actualValue)
        {
            actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
            var message = (HttpResponseMessage)actualValue.Subject;
            string content = null;
            if (message.Content != null)
            {
                content = await message.Content.ReadAsStringAsync();
            }

            message.StatusCode.Should().Be(HttpStatusCode.Forbidden, $"{message.ReasonPhrase} {content}");
        }

        public static async Task BeUnprocessableEntity(this ObjectAssertions actualValue)
        {
            actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
            var message = (HttpResponseMessage)actualValue.Subject;
            string content = null;
            if (message.Content != null)
            {
                content = await message.Content.ReadAsStringAsync();
            }

            message.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity, $"{message.ReasonPhrase} {content}");
        }

        public static async Task<string> BeNotFound(this ObjectAssertions actualValue)
        {
            actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
            var message = (HttpResponseMessage)actualValue.Subject;
            message.StatusCode.Should().Be(HttpStatusCode.NotFound);
            return await message.Content.ReadAsStringAsync();
        }

        public static async Task<string> BeBadRequest(this ObjectAssertions actualValue, string expectedContent = null)
        {
            actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
            var message = (HttpResponseMessage)actualValue.Subject;
            var content = await message.Content.ReadAsStringAsync();
            message.StatusCode.Should().Be(HttpStatusCode.BadRequest, content);

            if (expectedContent != null)
            {
                content.Should().Contain(expectedContent);
            }

            return content;
        }

        public static async Task<string> BeFailedDependency(this ObjectAssertions actualValue, string expectedContent = null)
        {
            actualValue.Subject.Should().BeOfType<HttpResponseMessage>();
            var message = (HttpResponseMessage)actualValue.Subject;
            var content = await message.Content.ReadAsStringAsync();
            message.StatusCode.Should().Be(HttpStatusCode.FailedDependency, content);

            if (expectedContent != null)
            {
                content.Should().Contain(expectedContent);
            }

            return content;
        }

        public static void Match<T>(this GenericCollectionAssertions<T> actualValue, IList<T> matchValue, Action<T, T> compare)
        {
            actualValue.NotBeNull();
            actualValue.NotBeEmpty();
            actualValue.HaveCount(matchValue.Count);
            if (matchValue.Count > 0 && compare != null)
            {
                var i = 0;
                foreach (var x in actualValue.Subject)
                {
                    compare.Invoke(x, matchValue[i++]);
                }
            }
        }

        public static void ContainErrorMessage(this GenericCollectionAssertions<ValidationFailure> actualValue, string errorMessage)
        {
            actualValue.Contain(x => x.ErrorMessage != null && x.ErrorMessage.Contains(errorMessage));
        }

        public static void HaveErrorMessageFor<T>(this ObjectAssertions actualValue, T obj,
            Expression<Func<T, object>> expression, string errorMessage = null)
        {
            var validationResult = actualValue.BeOfType<ValidationResult>().Subject;
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCountGreaterThan(0);
            var name = GetMemberName(expression);
            validationResult.Errors.Should().Contain(x => x.PropertyName == name
                                                          && x.ErrorMessage != null
                                                          && (errorMessage == null || x.ErrorMessage.Contains(errorMessage)));
        }

        private static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            var memberExpression = expression.Body switch
            {
                MemberExpression member => member,
                UnaryExpression unary => unary.Operand as MemberExpression,
                _ => null
            };
            if (memberExpression == null)
            {
                throw new ArgumentException($"Expression '{expression}' refers to a method, not a property.");
            }

            return memberExpression.Member.Name;
        }
    }
}