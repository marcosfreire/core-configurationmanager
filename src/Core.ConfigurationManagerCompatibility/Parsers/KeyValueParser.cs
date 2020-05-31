using Microsoft.Extensions.Configuration;
using Core.ConfigurationManagerCompatibility.Enums;
using Core.ConfigurationManagerCompatibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Core.ConfigurationManagerCompatibility.Parsers
{
    public class KeyValueParser : IConfigurationParser
    {
        private readonly string _keyName = "key";
        private readonly string _valueName = "value";

        private readonly string[] _operacoesSuportadas = Enum.GetNames(typeof(Operacoes)).Select(x => x.ToLowerInvariant()).ToArray();

        public KeyValueParser() : this("key", "value") { }

        public KeyValueParser(string key, string value)
        {
            _keyName = key;
            _valueName = value;
        }

        public bool PodeParsearElemento(XElement element)
        {
            var hasKeyAttribute = element.DescendantsAndSelf().Any(x => x.Attribute(_keyName) != null);
            return hasKeyAttribute;
        }

        public void ParsearElemento(XElement element, Stack<string> context, SortedDictionary<string, string> results)
        {
            if (!PodeParsearElemento(element))
            {
                return;
            }

            if (AdicionarConfiguracaoElemento(element))
            {
                AdicionarConfiguracaoElemento(element, context, results);
            }

            context.Push(element.Name.ToString());

            foreach (var node in element.Elements())
            {
                if (!OperacaoSuportada(node))
                {
                    continue;
                }

                ParsearElemento(node, context, results);
            }

            context.Pop();
        }

        private bool OperacaoSuportada(XElement node)
        {
            return node.DescendantsAndSelf().Any(x => _operacoesSuportadas.Contains(x.Name.ToString().ToLowerInvariant()));
        }

        private static bool AdicionarConfiguracaoElemento(XElement element)
        {
            return !element.Elements().Any();
        }

        private void AdicionarConfiguracaoElemento(XElement element, Stack<string> context, SortedDictionary<string, string> results)
        {
            if (!Enum.TryParse(element.Name.ToString(), true, out Operacoes action)) return;

            var key = element.Attribute(_keyName);
            var value = element.Attribute(_valueName);

            if (key == null)
            {
                return;
            }

            var nomeChaveCompletaConfig = RecuperarChave(context, key.Value);

            switch (action)
            {
                case Operacoes.Add:

                    string valorParaAdicioanr = null;

                    if (value != null)
                    {
                        valorParaAdicioanr = value.Value;
                    }

                    if (results.ContainsKey(nomeChaveCompletaConfig))
                    {
                        results[nomeChaveCompletaConfig] = valorParaAdicioanr;
                    }
                    else
                    {
                        results.Add(nomeChaveCompletaConfig, valorParaAdicioanr);
                    }
                    break;
                case Operacoes.Remove:
                    results.Remove(nomeChaveCompletaConfig);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported action: [{action}]");
            }
        }

        private static string RecuperarChave(Stack<string> context, string name)
        {
            return string.Join(ConfigurationPath.KeyDelimiter, context.Reverse().Concat(new[] { name }));
        }
    }
}