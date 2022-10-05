namespace ConsoleJob.Job.Infrastructure;

public class ExecutionInfo
{
  private readonly AppSettings _settings;
  private readonly List<(string, string)> _data = new();

  public ExecutionInfo(IOptions<AppSettings> settings) => _settings = settings.Value;

  public void Add(string folio, string remarks) => _data.Add(new(folio, remarks));

  public string Report(TimeSpan elapsed, Exception? exception)
  {
    var builder = new StringBuilder("<p>Hi Team,</p>");

    if (!_data.Any())
      builder.Append("<p>No records pulled for processing.</p>");

    if (_data.Any())
      builder.Append("<p>Table below contains SP list items that were processed.</p>")
             .Append(AsTable());

    if (exception is not null)
      builder.Append($"<p>Scheduled job '{AppDomain.CurrentDomain.FriendlyName}' failed, requires further checking.</p>")
             .Append($"<p><strong>Exception Message:</strong> {exception.Message}</p>")
             .Append($"<p><strong>Exception StackTrace:</strong> {exception.StackTrace}</p>");

    builder.Append(ResourcesAsTable(elapsed))
           .Append("<br/>Regards,<br/>Dev Support");

    return builder.ToString();
  }

  private string AsTable()
  {
    var builder = new StringBuilder();

    builder.Append("<table style='border: 1px solid black; border-collapse: collapse; width: 100%'>")
           .Append("<tr style='border: 1px solid black;'>")
           .Append("<th style='border: 1px solid black;'><strong>SP List ID</strong></th>")
           .Append("<th style='border: 1px solid black;'><strong>Remarks</strong></th>");

    _data.ForEach(record =>
    {
      var (itemId, remarks) = record;
      builder.Append("<tr style='border: 1px solid black;'>")
             .Append($"<td style='border: 1px solid black;'>{itemId}</td>")
             .Append($"<td style='border: 1px solid black;'>{remarks}</td>")
             .Append("</tr>");
    });

    builder.Append("</table>");

    return builder.ToString();
  }

  private string ResourcesAsTable(TimeSpan elapsed)
  {
    return $@"
        <p>Application Info:</p>
        <table style='border: 1px solid black; border-collapse: collapse; width: 100%'>
            <tr style='border: 1px solid black;'>
               <td style='border: 1px solid black;'>Microservice</td>
               <td style='border: 1px solid black;'>{_settings.Settings.Microservice.Url}</td>
            </tr>
            <tr style='border: 1px solid black;'>
                <td style='border: 1px solid black;'>Server</td>
                <td style='border: 1px solid black;'>{Environment.MachineName}</td>
            </tr>
            <tr style='border: 1px solid black;'>
                <td style='border: 1px solid black;'>App Directory</td>
                <td style='border: 1px solid black;'>{AppInfo.ContentRoot}</td>
            </tr>
            <tr style='border: 1px solid black;'>
                <td style='border: 1px solid black;'>Execution Time</td>
                <td style='border: 1px solid black;'>{Math.Round(elapsed.TotalMinutes, 2)} minute(s)</td>
            </tr>
        </table>";
  }
}
