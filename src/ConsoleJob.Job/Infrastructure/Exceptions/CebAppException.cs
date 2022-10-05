namespace ConsoleJob.Job.Infrastructure.Exceptions;

public class CebAppException : ApplicationException
{
  public CebAppException(string? message) : base(message) { }
}
