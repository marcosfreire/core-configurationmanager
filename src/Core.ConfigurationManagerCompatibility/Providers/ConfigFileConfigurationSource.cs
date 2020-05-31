using Core.ConfigurationManagerCompatibility.Interfaces;
using Core.ConfigurationManagerCompatibility.Parsers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Core.ConfigurationManagerCompatibility.Providers
{
    public class ConfigFileConfigurationSource : IConfigurationSource
    {
        public string ArquivoConfiguracao { get; set; }                
        public IEnumerable<IConfigurationParser> Parsers { get; set; }

        public ConfigFileConfigurationSource(string configuration)
        {
            ArquivoConfiguracao = configuration;
            Parsers = RecuperarParsers().ToArray();
        }

        private static List<IConfigurationParser> RecuperarParsers()
        {
            return new List<IConfigurationParser> {
                new KeyValueParser(),
                new KeyValueParser("name", "connectionString")
            };
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConfigFileConfigurationProvider(ArquivoConfiguracao, Parsers);
        }
    }
}
