using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;
using Nac.Lib.Utilities;
using Nac.Services.Logging.Interfaces;
using Nac.Services.Logging.Settings;
using Serilog;
using Serilog.Core.Enrichers;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace Nac.Services.Logging.Configuration;

public static class LoggingConfiguration
{
    public static IServiceCollection RegisterLoggingInterfaces(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogging<>), typeof(AppLogging<>));
        return services;
    }

    internal const string _outputTemplate =
        @"[{Timestamp:o} {Level}] {ApplicationName}:{SourceContext} | msg:'{Message}' | method:'{MemberName}' | {FilePath}:{LineNumber} | excptn:'{Exception}'{NewLine}";

    internal static readonly IDictionary<string, ColumnWriterBase> _columnOptions = new Dictionary<string, ColumnWriterBase>
{
    { "application_name", new SinglePropertyColumnWriter("ApplicationName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") },
    { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
    //{ "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
    { "level", new LevelColumnWriter(true, NpgsqlDbType.Text) },
    { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
    { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
    //{ "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
    { "properties", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
    { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") },
    { "file_path", new SinglePropertyColumnWriter("FilePath", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") },
    { "member_name", new SinglePropertyColumnWriter("MemberName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") },
    { "line_number", new SinglePropertyColumnWriter("LineNumber", PropertyWriteMethod.Raw, NpgsqlDbType.Integer) }
};

    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        ConfigureSerilog(builder.Logging, builder.Environment, builder.Configuration);
    }

    public static void ConfigureSerilog(ILoggingBuilder l, IHostEnvironment e, IConfiguration c)
    {
        l.ClearProviders();

        // read log settings from config file
        var config = c;
        var logSettings = config.GetSection(nameof(AppLoggingSettings)).Get<AppLoggingSettings>();
        var connectionStringName = logSettings.DbServer.ConnectionStringName;
        var connectionString = config.GetConnectionString(connectionStringName);
        var tableName = logSettings.DbServer.TableName;
        var schema = logSettings.DbServer.Schema;
        string restrictedToMinimumLevel = logSettings.General.RestrictedToMinimumLevel;
        if (!Enum.TryParse<LogEventLevel>(restrictedToMinimumLevel, out var logLevel))
        {
            logLevel = LogEventLevel.Debug;
        }

        var log = new LoggerConfiguration()
            .MinimumLevel.Is(logLevel)
            .Enrich.FromLogContext()
            .Enrich.With(new PropertyEnricher("ApplicationName", config.GetValue<string>("ApplicationName")))
            .Enrich.WithMachineName()
            .WriteTo.File(
                path: e.IsDevelopment()
                    ? logSettings.File.SubPathAndFileName
                    : logSettings.File.FullLogPathAndFileName, // "ErrorLog.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: logLevel,
                outputTemplate: _outputTemplate)
            .WriteTo.Console(restrictedToMinimumLevel: logLevel)
            .WriteTo.PostgreSQL(
                connectionString: connectionString!,
                tableName: tableName,
                columnOptions: _columnOptions,
                needAutoCreateTable: true,
                schemaName: "logging",
                useCopy: true,
                queueLimit: 3_000,
                batchSizeLimit: 40,
                period: new TimeSpan(0, 0, 10),
                restrictedToMinimumLevel: logLevel,
                failureCallback: e => Console.WriteLine($"Sink error: {e.Message}"),
                formatProvider: null);
        l.AddSerilog(log.CreateLogger(), false);
    }

    public static void LogWelcomeMessage(this IHost app)
    {
        ILogger<IHost> logger = app.Services.GetRequiredService<ILogger<IHost>>();
        logger.LogWelcomeMessage();
    }

    public static void LogWelcomeMessage(this Microsoft.Extensions.Logging.ILogger logger)
    {
        logger.LogInformation("{namedVersion} ({company} - {copyright}) started at {timestamp:u}",
                              AssemblyProperties.GetNamedVersion(),
                              AssemblyProperties.GetCompany(),
                              AssemblyProperties.GetCopyright(),
                              DateTime.UtcNow);
    }

}
