namespace ConsoleJob.Job.Application.Commands;

public record SampleCommand() : IRequest;

public class SampleCommandHandler : IRequestHandler<SampleCommand>
{
  private readonly ILogger<SampleCommandHandler> _logger;

  public SampleCommandHandler(ILogger<SampleCommandHandler> logger)
  {
    _logger = logger;
  }

  public async Task<Unit> Handle(SampleCommand command, CancellationToken cancellationToken)
  {
    await Task.Delay(1000, cancellationToken);
    return Unit.Value;
  }
}
