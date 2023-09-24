using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using EnigmaShared.Models;
using EnigmaShared.Constants;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;

namespace EnigmaShared.Configuration;

public static class ConfigurationExtension
{
    private const string ROOT_FOLDER = "ROOT_FOLDER";
    private static string _rootFolder = "";
    private static string _configFolder = "";
    private static string Env = "";
    public static string DLLVersion = "";

    public static IHostBuilder ConfigureAppsettings(this IHostBuilder builder, Type type, IEnumerable<string> files)
    {
        //DLLVersion = type.Assembly.GetName().Version.ToString();
        string configFolder = GetConfigFolder();
        var file = string.Empty;
        foreach (var item in files)
        {
            file  = Path.Combine(configFolder, item);
            AddFileConfig(builder, file);

        }
        return builder;
    }

    public static string GetConfigFolder()
    {
        if (string.IsNullOrEmpty(_configFolder))
        {
            var rootFolder = GetRootFolder();
            var configFolderName = GetConfigFolderName();
            _configFolder = Path.Combine(rootFolder, configFolderName);

            Console.WriteLine($"Config folder: {_configFolder}");
        }

        return _configFolder;
    }

    private static string GetConfigFolderName()
    {
        return "Config";
    }

    private static string GetRootFolder()
    {
        if (string.IsNullOrEmpty(_rootFolder))
        {
            string basePath = AppContext.BaseDirectory;
            string rootFolder = Environment.GetEnvironmentVariable(ROOT_FOLDER) ?? "";
            _rootFolder = Path.Combine(basePath, rootFolder);

            Console.WriteLine($"_rootFolder folder: {_rootFolder}");
        }

        return _rootFolder;
    }

    private static void AddFileConfig(IHostBuilder builder, string file)
    {
        if (File.Exists(file))
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile(file);
            });
        } else
        {
            Console.WriteLine($"Not found: {file}");
        }
    }

    public static TConfig InitConfig<TConfig>(this IServiceCollection services, IConfiguration configuration)
        where TConfig : DefaultConfig
    {
        var config = Activator.CreateInstance<TConfig>();
        new ConfigureFromConfigurationOptions<TConfig>(configuration).Configure(config);
        services.AddSingleton(config);
        services.AddSingleton((DefaultConfig)config);

        Console.WriteLine($" --- AppConfig: {JsonConvert.SerializeObject(config)}");
        Env = config.Env;

        return config;
    }

    private static SwaggerConfig _swaggerConfig = null;

    private static SwaggerConfig GetSwaggerConfig(IConfiguration configuration)
    {
        if (_swaggerConfig == null)
        {
            _swaggerConfig = ExtensionFactory.InjectConfig<SwaggerConfig>(configuration, "Swagger");

        }
        return _swaggerConfig;
    }


    public static void InitNewtonJson(this IMvcBuilder builder, IConfiguration configuration)
    {
        var config = GetSwaggerConfig(configuration);
        builder.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.DateFormatHandling = ConvertConfig.JSONDateFormatHandling;
            options.SerializerSettings.DateTimeZoneHandling = ConvertConfig.JSONDateTimeZoneHandling;
            options.SerializerSettings.DateFormatString = ConvertConfig.DateFormatString;
            options.SerializerSettings.NullValueHandling = ConvertConfig.JSONNullValueHandling;
            options.SerializerSettings.ReferenceLoopHandling = ConvertConfig.JSONReferenceLoopHandling;
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //options.SerializerSettings.Converters.Add(new CustomDateFormatConverter());
        });
    }

    public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var config = GetSwaggerConfig(configuration);

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(config.JSONPath, config.Name);
        });
            
    }

    public static void InjectContextService(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<IContextService, ContextService>();
    }

    public static void InjectSwagger(IServiceCollection services, IConfiguration configuration)
    {
        var config = GetSwaggerConfig(configuration);

        services.AddSwaggerGen(c => {
            c.SwaggerDoc(config.Version, new Microsoft.OpenApi.Models.OpenApiInfo { Title = config.Title, Version = config.Version });
        });
    }   
}

