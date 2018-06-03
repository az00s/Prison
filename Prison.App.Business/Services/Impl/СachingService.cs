using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Business.Services.Impl
{
    public class СachingService: ICachingService
    {
        private ILogger _log;

        private MemoryCache _cache;

        public СachingService(ILogger log)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            _log = log;
            _cache= MemoryCache.Default;

        }
        public T Get<T>(string key) where T:class
        {
            return _cache[key] as T;
        }

        public bool Add<T>(string key, T value, int seconds)
        {
            return _cache.Add(key, value, DateTime.Now.AddSeconds(seconds));
        }

        public void Update<T>(string key,T value,int seconds)
        {
            _cache.Set(key, value, DateTime.Now.AddSeconds(seconds));
        }

        public void Delete(string key)
        {
            if (_cache.Contains(key))
            {
                _cache.Remove(key);
            }
        }

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public T AddOrGetExisting<T>(string key,T value,int seconds) where T : class
        {
            return _cache.AddOrGetExisting(key,value,DateTime.Now.AddSeconds(seconds)) as T;
        }

    }
}
