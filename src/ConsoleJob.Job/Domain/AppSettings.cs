namespace ConsoleJob.Job.Domain;

public record AppSettings
{
  public ConnectionStrings ConnectionStrings { get; set; } = new();
  public Settings Settings { get; set; } = new();
}

public record ConnectionStrings
{
  public string LoggerDb { get; set; } = string.Empty;
}

public record Settings
{
  public Microservice Microservice { get; set; } = new();
  public SendGrid SendGrid { get; set; } = new();
}

public record Microservice
{
  public string Url { get; set; } = string.Empty;
  public string Authentication { get; set; } = string.Empty;
}

public record SendGrid
{
  public string Url { get; set; } = string.Empty;
  public string AccessPath { get; set; } = string.Empty;
  public string ServicePath { get; set; } = string.Empty;
  public string Subject { get; set; } = string.Empty;
  public string From { get; set; } = string.Empty;
  public string FromName { get; set; } = string.Empty;
  public List<string> Recipients { get; set; } = new();
  public string AuthorizationKey { get; set; } = string.Empty;
}
