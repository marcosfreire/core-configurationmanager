using Core.ConfigurationManagerCompatibility.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Core.ConfigurationManagerCompatibility.Providers
{
    public class ConfigFileConfigurationProvider : ConfigurationProvider
    {
        private readonly string _arquivoConfiguracao;

        private readonly IEnumerable<IConfigurationParser> _parsers;

        public ConfigFileConfigurationProvider(string configuration, IEnumerable<IConfigurationParser> parsers)
        {
            _parsers = parsers;
            _arquivoConfiguracao = configuration;
        }

        public override void Load()
        {
            var document = XDocument.Load(_arquivoConfiguracao);

            var context = new Stack<string>();
            var dictionary = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var child in document.Root.Elements())
            {
                ParseElement(child, context, dictionary);
            }

            Data = dictionary;
        }

        /// <summary>
        /// Irá fazer o parse do elemento xml com um dos parses previamente configurados
        /// e adiciona o valor para o dicionário de configurações
        /// </summary>
        private void ParseElement(XElement element, Stack<string> context, SortedDictionary<string, string> results)
        {
            var parser = RecuperarParser(element);

            if (CanParse(parser))
            {
                parser.ParsearElemento(element, context, results);
            }
        }

        private static bool CanParse(IConfigurationParser parser)
        {
            return parser != null;
        }

        private IConfigurationParser RecuperarParser(XElement element)
        {
            return _parsers.FirstOrDefault(a => a.PodeParsearElemento(element));
        }
    }
}