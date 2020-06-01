using Core.ConfigurationManagerCompatibility.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Core.ConfigurationManagerCompatibility
{
    public class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AdicionarArquivoConfiguracao("App.config")
                .AdicionarCompatibilidadeDotNetFullFramework();

            var arquivoConfig1 = ConfigurationManagerCore.AppSettings["AppConfig"];

            var arquivoConfigConnectionString1 = configuration.GetConnectionString("AppConfigConnection");
            var arquivoJsonConnectionString1 = configuration.GetConnectionString("JsonConfigSampleConnection");

            Console.WriteLine("\n## App.Config ##");
            Console.WriteLine(arquivoConfig1);

            Console.WriteLine("\n\n## App.Config - ConnectionString ##");
            Console.WriteLine(arquivoConfigConnectionString1);

            Console.WriteLine("\n\n## appsettings.Json##");
            Console.WriteLine(arquivoJsonConnectionString1);

            Console.ReadKey();
        }
    }
}
