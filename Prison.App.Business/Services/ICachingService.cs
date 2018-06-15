namespace Prison.App.Business.Services
{
    public interface ICachingService
    {
        T Get<T>(string key) where T : class;
        bool Add<T>(string key, T value, int seconds);
        void Update<T>(string key, T value, int seconds);
        void Delete(string key);
        bool Contains(string key);
        T AddOrGetExisting<T>(string key, T value, int seconds) where T : class;
    }
}
