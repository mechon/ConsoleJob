namespace ConsoleJob.Job.Infrastructure.StartupSetup;

internal static class AppConfiguration
{
  public static IHostBuilder SetAppConfig(this IHostBuilder builder)
    => builder.ConfigureAppConfiguration((ctx, cfg) =>
    {
      var basePath = ctx.HostingEnvironment.IsDevelopment() ? Directory.GetCurrentDirectory() : AppInfo.ContentRoot;

      cfg.SetBasePath(basePath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables(ctx.HostingEnvironment.ApplicationName)
        .Build();
    });
}
