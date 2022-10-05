namespace ConsoleJob.Job.Infrastructure.StartupSetup;

internal static class AppConfiguration
{
  public static void Set(HostBuilderContext context, IConfigurationBuilder configuration)
  {
    var basePath = context.HostingEnvironment.IsDevelopment() ? Directory.GetCurrentDirectory() : AppInfo.ContentRoot;

    configuration.SetBasePath(basePath)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
      .AddEnvironmentVariables(context.HostingEnvironment.ApplicationName)
      .Build();
  }
}
