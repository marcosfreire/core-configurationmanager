# Core.ConfigurationManager

Projeto para facilitar o uso da **maneira antiga** de acesso aos arquivos de configuração em projetos **netcore**

# Motivação

Possibilidade de **migrar projetos legados** para **.netcore** sem que seja preciso reescrever a maneira como os arquivos de configuração são usados.

# Começando

#### Para usar as configurações de *.config projetos .net core basta adicionar o passo abaixo em sua classe Startup.cs ou Program.cs:

```
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AdicionarArquivoConfiguracao("Web.config")
                .AdicionarCompatibilidadeDotNetFullFramework();
```

#### Após esse passo, é possivel acessar as configurações da maneira tradicional em projetos **.netcore**

```
var sample1 = ConfigurationManagerCore.AppSettings["Name"];
```

#### Também é possivel acessar as configurações de connection string de maneira tradicional:

```
var connectionString = ConfigurationManagerCore.ConnectionStrings["Name"];            
```

### É possivel também utilizar a interface IConfiguration para recuperar os dados dos arquivos *.config:
```
var appSettingsValue = configuration.GetAppSetting("AppConfig");
```

### É possivel usar multiplos arquivos de configuração (*.config e *.json)

```
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AdicionarArquivoConfiguracao("App.config")
                .AdicionarArquivoConfiguracao("Web.config")
                .AdicionarCompatibilidadeDotNetFullFramework();
```

## Referencias

[Referências obtidas de : github.com/aspnet/Entropy](https://github.com/aspnet/Entropy/tree/master/samples/Config.CustomConfigurationProviders.Sample)

# Em contrução ...
