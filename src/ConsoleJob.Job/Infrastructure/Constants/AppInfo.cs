namespace ConsoleJob.Job.Infrastructure.Constants;

internal static class AppInfo
{
  public static string? ContentRoot => Path.GetDirectoryName(Environment.ProcessPath);
  public static string EnvironmentUserName => $@"{Environment.UserDomainName}\{Environment.UserName}";
}
