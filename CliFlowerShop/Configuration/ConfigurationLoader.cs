using System.IO;
using Newtonsoft.Json;

namespace CliFlowerShop.Configuration
{
    public static class ConfigurationLoader<T>
    {
        public static T Load(string path) 
            => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}
