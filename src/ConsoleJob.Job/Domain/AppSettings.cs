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
  public Workflow Workflow { get; set; } = new();
  public Sharepoint Sharepoint { get; set; } = new();
  public SendGrid SendGrid { get; set; } = new();
}

public record Workflow
{
  public string Url { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
}

public record Sharepoint
{
  public string Url { get; set; } = string.Empty;
  public string ListName { get; set; } = string.Empty;
  public string FolioPrefix { get; set; } = string.Empty;
  public string CamlQuery { get; set; } = string.Empty;
  public Credential Credential { get; set; } = new();
}

public record Credential
{
  public string User { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Domain { get; set; } = string.Empty;
}

public record SendGrid
{
  public string Url { get; set; } = string.Empty;
  public string Subject { get; set; } = string.Empty;
  public string From { get; set; } = string.Empty;
  public string FromName { get; set; } = string.Empty;
  public List<string> Recipients { get; set; } = new();
  public string Token { get; set; } = string.Empty;
}
