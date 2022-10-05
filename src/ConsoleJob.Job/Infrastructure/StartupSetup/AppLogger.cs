namespace ConsoleJob.Job.Infrastructure.StartupSetup;

internal static class AppLogger
{
  private static void Init(string path) => _ = !Directory.Exists(path) ? Directory.CreateDirectory(path) : null;

  private static ColumnOptions BuildColumnOptions()
  {
    var columnOptions = new ColumnOptions();
    columnOptions.Store.Remove(StandardColumn.Properties);
    columnOptions.Store.Add(StandardColumn.LogEvent);
    columnOptions.AdditionalColumns = new Collection<SqlColumn>
        {
            new ("Application", System.Data.SqlDbType.NVarChar, false),
            new ("Server", System.Data.SqlDbType.NChar, false, 50),
            new ("EnvironmentUserName", System.Data.SqlDbType.NVarChar)
        };

    return columnOptions;
  }

  public static void Set(HostBuilderContext hostContext, LoggerConfiguration loggerConfiguration)
  {
    var appName = hostContext.HostingEnvironment.ApplicationName;
    var appSettings = JsonSerializer.Deserialize<AppSettings>(hostContext.Configuration[appName]);

    var logDirectory = $@"{AppInfo.ContentRoot}\Logs";
    Init(logDirectory);

    var file = File.CreateText($@"{logDirectory}\self.log");
    Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));

    loggerConfiguration.Enrich.WithProperty("Application", appName)
        .Enrich.WithProperty("Server", Environment.MachineName)
        .Enrich.WithProperty("EnvironmentUserName", AppInfo.EnvironmentUserName)
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
        .WriteTo.File
        (
            path: $@"{logDirectory}\log-.txt",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Verbose
        )
        .WriteTo.MSSqlServer
        (
            connectionString: appSettings!.ConnectionStrings.LoggerDb,
            sinkOptions: new MSSqlServerSinkOptions
            {
              TableName = "Events",
              AutoCreateSqlTable = false
            },
            columnOptions: BuildColumnOptions(),
            restrictedToMinimumLevel: LogEventLevel.Information
        );
  }
}
