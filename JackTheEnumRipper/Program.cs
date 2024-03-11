using System;

using JackTheEnumRipper.Core;
using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

using McMaster.Extensions.CommandLineUtils;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

using NLog;
using NLog.Extensions.Logging;

using ILogger = NLog.Logger;

namespace JackTheEnumRipper
{
    public class Program
    {
        private readonly ILogger? _logger;

        private readonly IHostEnvironment _environment;

        private readonly IServiceProvider? _serviceProvider;

        private readonly IConfigurationRoot? _configurationRoot;

        public Program()
        {
            this._environment = new HostingEnvironment
            {
                ApplicationName = Project.Name,
                EnvironmentName = Project.CompileTimeEnvironment,
                ContentRootPath = Project.BasePath
            };

            this._configurationRoot = new ConfigurationBuilder()
                .ConfigureAppBuilder(this._environment)
                .Build();

            this._serviceProvider = new ServiceCollection()
                .ConfigureAppServices(this._environment, this._configurationRoot)
                .BuildServiceProvider();

            LogManager.Configuration = new NLogLoggingConfiguration(this._configurationRoot.GetSection("NLog"));
            this._logger = LogManager.GetCurrentClassLogger();
        }

        public static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }

        public void Run(string[] args)
        {
            try
            {
                var cli = new CommandLineApplication
                {
                    Name = Project.Name,
                    Description = Project.Description,
                };

                cli.HelpOption(inherited: true);
                var version = cli.Option("-v|--version", "display program version and exit", CommandOptionType.NoValue);

                cli.Command("export", exportCommand =>
                {
                    exportCommand.Description = "provide one or more export format";
                    var listOption = exportCommand.Option("--list", "list all available export formats and exit", CommandOptionType.NoValue);
                    var formatOption = exportCommand.Option("--format", "the name of a format writer", CommandOptionType.SingleValue);

                    exportCommand.OnExecute(() =>
                    {
                        if (listOption.HasValue())
                        {
                            var serializerService = this._serviceProvider?.GetService<ISerializerService>();
                            var availableFormats = serializerService?.GetAvailableFormats();
                            Console.WriteLine(string.Join(",", availableFormats!));
                            Environment.Exit(0);
                        }

                        if (formatOption.HasValue())
                        {
                            string? requestedFormat = formatOption.Value();
                            bool valid = Enum.TryParse(requestedFormat, ignoreCase: true, out Format format);

                            if (!valid) throw new ArgumentException("invalid format type", nameof(requestedFormat));

                            var serializerService = this._serviceProvider?.GetService<ISerializerService>();
                            serializerService?.Export(format);
                        }
                    });
                });

                cli.OnExecute(() =>
                {
                    if (version.HasValue())
                    {
                        Console.WriteLine($"{cli.Name}, version {Project.Version}");
                    }
                    else
                    {
                        cli.ShowHelp();
                    }
                });

                _ = cli.Execute(args);
            }
            catch (Exception exception)
            {
                _logger?.Error(exception);
                Console.Error.WriteLine("invalid arguments");
            }
            finally
            {
                LogManager.Shutdown();
                Environment.Exit(0);
            }
        }
    }
}
