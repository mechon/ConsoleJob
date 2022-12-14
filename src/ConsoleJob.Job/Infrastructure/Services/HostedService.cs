namespace ConsoleJob.Job.Infrastructure.Services;

public class HostedService : IHostedService
{
  private const int ErrorInvalidFunction = 0x1;

  private readonly ILogger<HostedService> _logger;
  private readonly IHostApplicationLifetime _lifetime;
  private readonly JobSteps _jobSteps;
  private readonly Stopwatch _timer;

  public HostedService(ILogger<HostedService> logger, IHostApplicationLifetime lifetime, JobSteps jobSteps)
  {
    _logger = logger;
    _lifetime = lifetime;
    _jobSteps = jobSteps;

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

    await _jobSteps.EmailReportAsync(_timer.Elapsed, cancellationToken);

    Log.CloseAndFlush();
  }

  private async Task Execute(CancellationToken cancellationToken)
  {
    try
    {
      await _jobSteps.StepOne(cancellationToken)
        .Then(_jobSteps.StepTwo, cancellationToken);
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, "Something went wrong!");
      _jobSteps.Exception = exception;
      Environment.ExitCode = ErrorInvalidFunction;
    }
    finally
    {
      _lifetime.StopApplication();
    }
  }
}
