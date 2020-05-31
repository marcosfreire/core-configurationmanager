using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Immutable;

namespace Core.ConfigurationManagerCompatibility.Configuration
{
    public static class ConfigurationExtensions
    {
        public const string AppSettings = "appSettings";

        public static string GetAppSetting(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection(AppSettings)[name];
        }
        
        /// <summary>
        /// Recupera todas as configurações para as sessões informadas
        /// </summary>
        /// <param name="sectionNames">Sessoes para obtenção das configurações.</param>
        public static ImmutableDictionary<string, IConfigurationSection> GetSection(this IConfiguration configuration, params string[] sectionNames)
        {
            if (sectionNames.Length == 0)
                return ImmutableDictionary<string, IConfigurationSection>.Empty;

            var fullKey = string.Join(ConfigurationPath.KeyDelimiter, sectionNames);

            return configuration?.GetSection(fullKey).GetChildren()?.ToImmutableDictionary(x => x.Key, x => x);
        }

        /// <summary>
        /// Recupera o valor para a chave informada
        /// </summary>
        /// <param name="keys">Coleção de chaves</param>
        /// <returns>Valor da chave informada</returns>
        public static string GetValue(this IConfiguration configuration, params string[] keys)
        {
            if (keys.Length == 0)
            {
                throw new ArgumentException("Need to provide keys", nameof(keys));
            }

            var fullKey = string.Join(ConfigurationPath.KeyDelimiter, keys);

            return configuration?[fullKey];
        }
    }
}