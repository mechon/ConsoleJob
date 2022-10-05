namespace ConsoleJob.Job.Infrastructure.Services;

public class HostedService : IHostedService
{
  private const int ErrorInvalidFunction = 0x1;

  private readonly ILogger<HostedService> _logger;
  private readonly IHostApplicationLifetime _lifetime;
  private readonly JobProcess _jobProcess;
  private readonly Stopwatch _timer;

  public HostedService(ILogger<HostedService> logger, IHostApplicationLifetime lifetime, JobProcess jobProcess)
  {
    _logger = logger;
    _lifetime = lifetime;
    _jobProcess = jobProcess;

    _timer = new Stopwatch();
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    _timer.Start();
    _logger.LogInformation("Job is running");

    async void Callback() => await Execute(cancellationToken);
    _lifetime.ApplicationStarted.Register(Callback);

    return Task.CompletedTask;
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    _timer.Stop();
    _logger.LogInformation("Job is stopping");

    await _jobProcess.EmailReportAsync(_timer.Elapsed, cancellationToken);

    Log.CloseAndFlush();
  }

  private async Task Execute(CancellationToken cancellationToken)
  {
    try
    {
      await _jobProcess.StepOne(cancellationToken)
                       .Then(cancellationToken, _jobProcess.StepTwo);
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, "Something went wrong!");
      _jobProcess.Exception = exception;
      Environment.ExitCode = ErrorInvalidFunction;
    }
    finally
    {
      _lifetime.StopApplication();
    }
  }
}
