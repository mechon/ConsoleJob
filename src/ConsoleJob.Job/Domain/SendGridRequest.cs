namespace ConsoleJob.Job.Domain;

public class SendGridRequest
{
  public IEnumerable<Personalization> personalizations { get; }
  public Person from { get; }
  public IEnumerable<Content> content { get; }

  public SendGridRequest(SendGrid settings, string message)
  {
    var to = settings.Recipients.Select(r => new Person(r, string.Empty));
    var personalization = new Personalization(to, settings.Subject);

    personalizations = new List<Personalization> { personalization };
    from = new Person(settings.From, settings.FromName);
    content = new List<Content> { new("text/html", message) };
  }
}

public record Personalization(IEnumerable<Person> to, string subject);

public record Person(string email, string name);

public record Content(string type, string value);
