namespace ConsoleJob.Job.Application.Notifications;

public record SendEmailNotification(string Message) : INotification;

public class SendEmailHandler : INotificationHandler<SendEmailNotification>
{
  private readonly ISendGridService _service;
  private readonly SendGrid _settings;

  public SendEmailHandler(ISendGridService service, IOptions<AppSettings> settings)
  {
    _service = service;
    _settings = settings.Value.Settings.SendGrid;
  }

  public async Task Handle(SendEmailNotification notification, CancellationToken cancellationToken)
  {
    var content = new SendGridRequest(_settings, notification.Message);
    _ = await _service.Send(content, cancellationToken);
  }
}
