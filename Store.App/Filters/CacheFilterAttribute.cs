using System;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Store.App.Filters
{
    public class CacheFilterAttribute : Attribute, IResourceFilter
    {
        private readonly IMemoryCache cache;

        public CacheFilterAttribute(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var result = context.Result;

            if (result is ObjectResult @object)
            {
                var pathAndQuery = context.HttpContext.Request.GetEncodedPathAndQuery();
                cache.Set(pathAndQuery, @object.Value,
                    TimeSpan.FromSeconds(30));
                Console.WriteLine($"Result for {pathAndQuery} was added to cache");
            }
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var pathAndQuery = context.HttpContext.Request.GetEncodedPathAndQuery();
            if (cache.TryGetValue(pathAndQuery, out var value))
            {
                Console.WriteLine($"Result for {pathAndQuery} has been extracted from cache");
                context.Result = new ObjectResult(value);
            }
            else
            {
                Console.WriteLine($"Nothing found in cache for {pathAndQuery}");
            }
        }
    }
}