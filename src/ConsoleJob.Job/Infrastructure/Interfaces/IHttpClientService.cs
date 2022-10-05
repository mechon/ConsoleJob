namespace ConsoleJob.Job.Infrastructure.Interfaces;

public interface IHttpClientService
{
  IHttpClientService CreateClient(string name);
  Task<string> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
  IHttpClientService AddCustomHeader(string key, string value);
}
