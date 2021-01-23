using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Galytix.WebApi.Controllers
{
    [ApiController]
    [Route("/api/gwp")]
    public class CountryGwpController : Controller
    {
        private static readonly IMemoryCache memoryCache  = new MemoryCache(new MemoryCacheOptions());
        private static readonly MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));


        [AllowAnonymous]
        [HttpGet]
        [Route("avg")]
        public async Task<IActionResult> Avg(string country, string lobs)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (country == null || country == string.Empty || lobs == null || lobs == string.Empty)
                return Ok(Json(result));

            var cacheKey = GetCacheKey(country, lobs);
            bool existsInCache = memoryCache.TryGetValue(cacheKey, out result);
            if (existsInCache)
                return Ok(Json(result));
            result = GetCountryGwp().GWP(country, lobs != null ? lobs.Split(',') : new string[] { });
            memoryCache.Set(cacheKey, result, cacheEntryOptions);
            return Ok(Json(result));
        }

        private ICountryGwp GetCountryGwp()
        {
            return new CountryGwp();
        }
        private string GetCacheKey(string country, string lobs)
        {
            return string.Concat("CountryGwp", country, lobs);
        }
    }
}
