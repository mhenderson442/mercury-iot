using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Text.Json;
using System;

namespace Mercuryiot.Test.Functions
{
    public class TestFactory
    {
        public static async Task<DefaultHttpRequest> CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(await CreateDictionary(queryStringKey, queryStringValue))
            };

            return request;
        }

        private static async Task<Dictionary<string, StringValues>> CreateDictionary(string key, string value)
        {
            await Task.Yield();

            var dictionary = new Dictionary<string, StringValues>
            {
                { key, value }
            };

            return dictionary;
        }

        public static async Task<Mock<HttpRequest>> CreateMockRequestAsync(object body = null)
        {
            await Task.Yield();

            var ms = new MemoryStream();

            if(body != null)
            {
                
                var sw = new StreamWriter(ms);

                var json = JsonSerializer.Serialize(body);

                sw.Write(json);
                sw.Flush();

                ms.Position = 0;
            }

            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Body).Returns(ms);

            return mockRequest;
        }

        public static async Task<Client> CreateMockClientAsync()
        {
            await Task.Yield();

            var client = new Client
            {
                Key = Guid.NewGuid().ToString(),
                Name = "Unit Test Client",
                Region = "US West",
                ttl = 600
            };

            return client;
        }

        public static Client CreateMockClient()
        {
            var client = new Client
            {
                Key = Guid.NewGuid().ToString(),
                Name = "Unit Test Client",
                Region = "US West",
                ttl = 600
            };

            return client;
        }
    }
}
