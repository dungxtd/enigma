using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EnigmaShared
{
    public static class ExtensionFactory
    {
        public static TConfig InjectConfig<TConfig>(IConfiguration configuration, string configSection, IServiceCollection services = null)
        {
            var config = configuration.GetSection(configSection).Get<TConfig>();
            Console.WriteLine($"--- {configSection}: {JsonConvert.SerializeObject(config)}");
            if (config == null)
            {
                throw new Exception($"Missing config {configSection}");
            }
            if (services != null)
            {
                services.AddSingleton(typeof(TConfig), config);
            }
            return config;
        }
    }
}

