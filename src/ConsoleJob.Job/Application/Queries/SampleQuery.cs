namespace ConsoleJob.Job.Application.Queries;

public record SampleQuery() : IRequest<IEnumerable<string>>;

public class SampleQueryHandler : IRequestHandler<SampleQuery, IEnumerable<string>>
{
  private readonly ILogger<SampleQueryHandler> _logger;

  public SampleQueryHandler(ILogger<SampleQueryHandler> logger) => _logger = logger;

  public async Task<IEnumerable<string>> Handle(SampleQuery query, CancellationToken cancellationToken)
  {
    await Task.Delay(1000, cancellationToken);

    return Enumerable.Empty<string>();
  }
}
