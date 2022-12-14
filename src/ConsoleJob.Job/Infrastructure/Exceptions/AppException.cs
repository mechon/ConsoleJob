using System.Runtime.Serialization;

namespace ConsoleJob.Job.Infrastructure.Exceptions;

[Serializable]
public class AppException : ApplicationException
{
  public AppException(string? message) : base(message) { }

  protected AppException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
}
