using System.Collections.Generic;
using System.Xml.Linq;

namespace Core.ConfigurationManagerCompatibility.Interfaces
{
    public interface IConfigurationParser
    {
        bool PodeParsearElemento(XElement element);        
        void ParsearElemento(XElement element, Stack<string> context, SortedDictionary<string, string> results);
    }
}