using System;
using System.Collections.Generic;

using JackTheEnumRipper.Factories;
using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;
using JackTheEnumRipper.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

using Serializer;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace JackTheEnumRipper.Core
{
    public static class Startup
    {
        public static IConfigurationBuilder ConfigureAppBuilder(this IConfigurationBuilder builder, IHostEnvironment environment)
        {
            return builder.SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
        }

        public static IServiceCollection ConfigureAppServices(this IServiceCollection services, IHostEnvironment envionment, IConfigurationRoot configurationRoot)
        {
            services.AddSingleton(envionment);
            services.Configure<AppSettings>(configurationRoot.GetSection(nameof(AppSettings)));

            services.AddLogging(logBuilder =>
            {
                logBuilder.ClearProviders();
                logBuilder.SetMinimumLevel(string.Equals(envionment.EnvironmentName, Environments.Development) ? LogLevel.Debug : LogLevel.Error);
                logBuilder.AddNLog();
            });

            services.AddSerializerFactory();
            services.AddSingleton<ISerializerService, SerializerService>();
            services.AddSingleton<IExtractorService, ExtractorService>();

            return services;
        }

        public static void AddSerializerFactory(this IServiceCollection services)
        {
            services.AddTransient<ISerializer, CSharpSerializer>();
            services.AddTransient<ISerializer, IniSerializer>();
            services.AddTransient<ISerializer, JsonSerializer>();
            services.AddTransient<ISerializer, PhpSerializer>();
            services.AddTransient<ISerializer, RustSerializer>();

            services.AddSingleton<Func<IEnumerable<ISerializer>>>(x => () => x.GetService<IEnumerable<ISerializer>>()!);

            services.AddSingleton<ISerializerFactory, SerializerFactory>();
        }
    }
}
