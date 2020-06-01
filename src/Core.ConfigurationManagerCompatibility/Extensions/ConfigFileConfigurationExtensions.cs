using Core.ConfigurationManagerCompatibility.Providers;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Core.ConfigurationManagerCompatibility.Configuration
{
    public static class ConfigFileConfigurationExtensions
    {
        /// <summary>
        /// Adiciona configuração dos arquivo *.config ao ConfigurationBuilder
        /// </summary>
        /// <param name="builder">Builder para adicionar as configurações</param>
        /// <param name="arquivoConfig">Caminho do arquivo *.config</param>
        /// <param name="optional">Flag indicando se o arquivo é opcional</param>
        /// <param name="parsers">Parsers para arquivos de configuracao</param>
        public static IConfigurationBuilder AdicionarArquivoConfiguracao(this IConfigurationBuilder builder, string arquivoConfig)
        {
            return builder.AddFile(arquivoConfig);
        }

        public static IConfiguration AdicionarCompatibilidadeDotNetFullFramework(this IConfigurationBuilder builder)
        {
            var configuration = builder.Build();
            ConfigurationManagerCore.AppSettings = new AppSettings(configuration);
            ConfigurationManagerCore.ConnectionStrings = new ConnectionStrings(configuration);

            return configuration;
        }

        private static IConfigurationBuilder AddFile(this IConfigurationBuilder builder, string arquivoConfig)
        {
            if (ConfiguracaoInvalida(arquivoConfig))
            {
                throw new ArgumentException("O arquivo para configuração não pode ser null", nameof(arquivoConfig));
            }

            return builder.Add(new ConfigFileConfigurationSource(arquivoConfig));
        }

        private static bool ConfiguracaoInvalida(string arquivoConfig)
        {
            return arquivoConfig == null || string.IsNullOrEmpty(arquivoConfig) || !File.Exists(arquivoConfig);
        }
    }
}
