namespace ConsoleJob.Job.Infrastructure.Services;

internal class HttpClientService : IHttpClientService
{
  private readonly IHttpClientFactory _httpClientFactory;
  private HttpClient? _httpClient;

  public HttpClientService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

  public IHttpClientService CreateClient(string name)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException($"Client name is required: '{name}'");

    _httpClient = _httpClientFactory.CreateClient(name);

    return this;
  }

  public async Task<string> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
  {
    if (_httpClient is null)
      throw new CebAppException("Http client is not set.");

    var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);

    return await HandleResponse(response);
  }

  public IHttpClientService AddCustomHeader(string key, string value)
  {
    if (_httpClient is null)
      throw new CebAppException("Http client is not set.");

    _httpClient.DefaultRequestHeaders.Add(key, value);

    return this;
  }

  private static async Task<string> HandleResponse(HttpResponseMessage message)
  {
    var content = await message.Content.ReadAsStringAsync();

    if (!message.IsSuccessStatusCode)
      throw new CebAppException($"{message.ReasonPhrase}\nContent: {content}");

    return content;
  }
}
