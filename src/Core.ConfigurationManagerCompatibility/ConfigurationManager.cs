using Core.ConfigurationManagerCompatibility.Configuration;
using Microsoft.Extensions.Configuration;

namespace System.Configuration
{
    public static class ConfigurationManager
    {
        public static AppSettings AppSettings { get; set; }
        public static ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        private readonly IConfiguration _configuration;

        public ConnectionStrings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string this[string key]
        {
            get
            {
                return _configuration.GetConnectionString(key);
            }
        }
    }

    public class AppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string this[string key]
        {
            get
            {
                return _configuration.GetAppSetting(key);
            }
        }
    }
}