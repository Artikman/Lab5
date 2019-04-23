using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Lab_4.Services;
using Lab_4.Models;

namespace Lab_4.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private string _cacheKey;

        public CacheMiddleware(RequestDelegate next, IMemoryCache memoryCache, string cacheKey = "Cinema")
        {
            _next = next;
            _memoryCache = memoryCache;
            _cacheKey = cacheKey;
        }

        public Task Invoke(HttpContext httpContext, DbService service)
        {
            List<Cinema> list;

            if (!_memoryCache.TryGetValue(_cacheKey, out list))
            {
                list = service.GetCinemas();

                _memoryCache.Set(_cacheKey, list,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2 * 8 + 240)));
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseCacheMiddleware(this IApplicationBuilder builder, string key)
        {
            return builder.UseMiddleware<CacheMiddleware>(key);
        }
    }

    public static class CacheMiddlewareExtension
    {
        public static IApplicationBuilder UseDbInitMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CacheMiddleware>();
        }
    }

    public static class DbInitMiddlewareExtensions
    {
        public static IApplicationBuilder UseBrowserLink(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CacheMiddleware>();
        }
    }
}