using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace ShopApp.Api.Infrastructure.ContentNegotiators
{
    public class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter jsonFormatter)
        {
            this.jsonFormatter = jsonFormatter;
        }

        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(this.jsonFormatter, new MediaTypeHeaderValue("application/json"));

            return result;
        }
    }
}