

namespace Genesis.Service
{
    public class SecureStorageService : ISecureStorageService
    {
        public SecureStorageService() { }
        public async Task<string> Get(string key)
        {
            return await SecureStorage.Default.GetAsync(key);
        }

        public async Task Save(string key, string value)
        {
            await SecureStorage.Default.SetAsync(key, value);
        }
    }
}
