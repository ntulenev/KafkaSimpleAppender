using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Serilog;

using Logic.Configuration;
using Logic.Configuration.Validation;
using Logic;

using KafkaSimpleAppender;

// It is unfortunate but we have to set it to Unknown first.
Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

Application.SetHighDpiMode(HighDpiMode.SystemAware);
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);

var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json");
var config = configuration.Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<UI>();
serviceCollection.AddSingleton<IKafkaSender, KafkaSender>();
serviceCollection.AddSingleton<IKafkaSendHandler, KafkaSendHandler>();
serviceCollection.AddSingleton<IJsonValidator, JsonValidator>();
serviceCollection.AddSingleton<IProducerBuilder, SimpleProducerBuilder>();
serviceCollection.AddSingleton<IFileLoader, FileLoader>();
serviceCollection.Configure<BootstrapConfiguration>(config.GetSection(nameof(BootstrapConfiguration)));
serviceCollection.Configure<FileLoaderConfiguration>(config.GetSection(nameof(FileLoaderConfiguration)));
serviceCollection.AddSingleton<IValidateOptions<BootstrapConfiguration>, BootstrapConfigurationValidator>();
serviceCollection.AddSingleton<IValidateOptions<FileLoaderConfiguration>, FileLoaderConfigurationValidator>();
var logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(config)
                 .CreateLogger();
serviceCollection.AddLogging(x =>
{
    x.SetMinimumLevel(LogLevel.Information);
    x.AddSerilog(logger: logger, dispose: true);
});
var provider = serviceCollection.BuildServiceProvider();

using (var serviceScope = provider.CreateScope())
{
    var scopeProvider = serviceScope.ServiceProvider;

    try
    {
        var form = scopeProvider.GetRequiredService<KafkaSimpleAppender.UI>();
        Application.Run(form);
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
