namespace ConsoleJob.Job.Infrastructure.StartupSetup;

internal static class AppServices
{
  public static IHostBuilder AddCustomServices(this IHostBuilder builder)
    => builder.ConfigureServices((ctx, svc) =>
    {
      var appName = ctx.HostingEnvironment.ApplicationName;
      var appSettings = JsonSerializer.Deserialize<AppSettings>(ctx.Configuration[appName]);

      if (appSettings is null)
        throw new AppException($"Environment Variable '{appName}' not set");

      svc.Configure<AppSettings>(ctx.Configuration);

      svc.AddHttpClient(SendGridService.AccessClient, client =>
      {
        var baseUrl = ctx.Configuration.GetValue<string>("Settings:SendGrid:Url");
        var authPath = ctx.Configuration.GetValue<string>("Settings:SendGrid:AuthenticationPath");
        client.BaseAddress = new Uri($"{baseUrl}{authPath}");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("Authorization", $"Basic {appSettings.Settings.SendGrid.AuthorizationKey}");
      });
      svc.AddHttpClient(SendGridService.MailClient, client =>
      {
        var baseUrl = ctx.Configuration.GetValue<string>("Settings:SendGrid:Url");
        var servicePath = ctx.Configuration.GetValue<string>("Settings:SendGrid:ServicePath");
        client.BaseAddress = new Uri($"{baseUrl}{servicePath}");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
      });

      svc.AddMediatR(Assembly.GetExecutingAssembly());
      svc.AddSingleton<ExecutionInfo>();
      svc.AddSingleton<JobSteps>();
      svc.AddScoped<IHttpClientService, HttpClientService>();
      svc.AddScoped<ISendGridService, SendGridService>();
      svc.AddHostedService<HostedService>();
    });
}
