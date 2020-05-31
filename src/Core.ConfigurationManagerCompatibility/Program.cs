using Core.ConfigurationManagerCompatibility.Configuration;
using Microsoft.Extensions.Configuration;
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
                .AdicionarArquivoConfiguracao("Web.config")
                .AdicionarCompatibilidadeDotNetFullFramework();

            var arquivoConfig1 = System.Configuration.ConfigurationManager.AppSettings["AppConfig"];
            var arquivoConfig2 = System.Configuration.ConfigurationManager.AppSettings["WebConfigAppSettings"];

            var arquivoConfig5 = configuration.GetAppSetting("AppConfig");

            var arquivoConfigConnectionString1 = System.Configuration.ConfigurationManager.ConnectionStrings["WebConfigConnection"];
            var arquivoConfigCconnectionString2 = System.Configuration.ConfigurationManager.ConnectionStrings["AppConfigConnection"];
            var arquivoConfigCconnectionString3 = System.Configuration.ConfigurationManager.ConnectionStrings["JsonConfigSampleConnection"];

            var arquivoJsonConnectionString1 = configuration.GetConnectionString("WebConfigConnection");
            var arquivoJsonConnectionString2 = configuration.GetConnectionString("AppConfigConnection");
            var arquivoJsonConnectionString3 = configuration.GetConnectionString("JsonConfigSampleConnection");
        }
    }
}
