namespace ConsoleJob.Job.Infrastructure.Services;

internal class SendGridService : ISendGridService
{
  public const string AccessClient = "SendGridAccess";
  public const string MailClient = "SendGridMail";

  private readonly IHttpClientService _client;

  public SendGridService(IHttpClientService client) => _client = client;

  private async Task<string> GetAccess(CancellationToken cancel)
  {
    var payload = new StringContent(string.Empty, Encoding.UTF8, "application/json");
    var response = await _client.CreateClient(AccessClient)
                                .PostAsync(string.Empty, payload, cancel);
    var content = JsonSerializer.Deserialize<JsonObject>(response);

    return (string)content!["accessToken"]!["accessToken"]!;
  }

  public async Task<string> Send(object content, CancellationToken cancellationToken)
  {
    if (content is null)
      throw new ArgumentNullException($"Email content is required: '{content}'");

    var accessToken = await GetAccess(cancellationToken);
    var serialized = JsonSerializer.Serialize(content);
    var payload = new StringContent(serialized, Encoding.UTF8, "application/json");

    return await _client.CreateClient(MailClient)
                        .AddCustomHeader("Authorization", $"Bearer {accessToken}")
                        .PostAsync(requestUri: string.Empty, payload, cancellationToken);
  }
}
