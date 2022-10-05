using ConsoleJob.Job.Application.Queries;

namespace ConsoleJob.Job.Infrastructure.Services;

public class JobProcess
{
  private readonly IMediator _mediator;
  private readonly ExecutionInfo _info;

  public Exception? Exception { get; set; }

  public JobProcess(IMediator mediator, ExecutionInfo info)
  {
    _mediator = mediator;
    _info = info;
  }

  public async Task<IEnumerable<string>> StepOne(CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SampleQuery(), cancellationToken);
  }

  public async Task StepTwo(CancellationToken cancellationToken)
  {
    await _mediator.Send(new SampleCommand(), cancellationToken);
  }

  public async Task EmailReportAsync(TimeSpan elapsed, CancellationToken cancel)
  {
    var message = _info.Report(elapsed, Exception);
    await _mediator.Publish(new SendEmailNotification(message), cancel);
  }
}
