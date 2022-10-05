namespace ConsoleJob.Job.Infrastructure.Interfaces;

public interface ISendGridService
{
  Task<string> Send(object content, CancellationToken cancellationToken);
}
