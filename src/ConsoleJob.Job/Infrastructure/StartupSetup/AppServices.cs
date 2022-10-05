namespace ConsoleJob.Job.Infrastructure.StartupSetup;

internal static class AppServices
{
  public static void Set(HostBuilderContext ctx, IServiceCollection svc)
  {
    var appName = ctx.HostingEnvironment.ApplicationName;
    var appSettings = JsonSerializer.Deserialize<AppSettings>(ctx.Configuration[appName]);

    if (appSettings is null)
      throw new CebAppException($"Environment Variable '{appName}' not set");

    svc.Configure<AppSettings>(ctx.Configuration);

    svc.AddHttpClient(SendGridService.AccessClient, client =>
    {
      client.BaseAddress = new Uri($"{ctx.Configuration.GetValue<string>("Settings:SendGrid:Url")}v1/oauth2/accessToken");
      client.DefaultRequestHeaders.Add("Accept", "application/json");
      client.DefaultRequestHeaders.Add("Authorization", $"Basic {appSettings.Settings.SendGrid.Token}");
    });
    svc.AddHttpClient(SendGridService.MailClient, client =>
    {
      var uri = new Uri($"{ctx.Configuration.GetValue<string>("Settings:SendGrid:Url")}ceb-sendgrid/send");
      client.BaseAddress = uri;
      client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    svc.AddMediatR(Assembly.GetExecutingAssembly());
    svc.AddSingleton<ExecutionInfo>();
    svc.AddSingleton<JobProcess>();
    svc.AddScoped<IHttpClientService, HttpClientService>();
    svc.AddScoped<ISendGridService, SendGridService>();
    svc.AddHostedService<HostedService>();
  }
}
