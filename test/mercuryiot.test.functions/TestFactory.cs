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
        public static async Task<DefaultHttpRequest> CreateClientHttpRequestAsync(string key, string value)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(await CreateClientDictionaryAsync(key, value))
            };

            return request;
        }

        public static async Task<DefaultHttpRequest> CreateRegionHttpRequestAsync(string key, string value)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection(await CreateRegionDictionaryAsync(key, value))
            };

            return request;
        }

        private static async Task<Dictionary<string, StringValues>> CreateClientDictionaryAsync(string key, string value)
        {
            await Task.Yield();

            var dictionary = new Dictionary<string, StringValues>
            {
                { key, value },
                { "region", "West US" }
            };

            return dictionary;
        }

        private static async Task<Dictionary<string, StringValues>> CreateRegionDictionaryAsync(string key, string value)
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
                id = Guid.NewGuid().ToString(),
                Name = "Unit Test Client",
                Region = "West US"
            };

            return client;
        }

        public static Client CreateMockClient()
        {
            var client = new Client
            {
                id = Guid.NewGuid().ToString(),
                Name = "Unit Test Client",
                Region = "US West",
                ttl = 600
            };

            return client;
        }
    }
}
