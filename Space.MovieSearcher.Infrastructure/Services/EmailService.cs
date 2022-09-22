using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Space.MovieSearcher.Infrastructure.Options;

namespace Space.MovieSearcher.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettingsOptions)
    {
        _smtpSettings = smtpSettingsOptions.Value;
    }

    public async Task SendAsync(string email, string subject, string htmlMessage)
    {
        var builder = new BodyBuilder { HtmlBody = htmlMessage };

        var emailMessage = new MimeMessage
        {
            Subject = subject,
            Body = builder.ToMessageBody()
        };

        emailMessage.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
        emailMessage.To.Add(MailboxAddress.Parse(email));

        await SendMessageAsync(emailMessage);
    }

    private async Task SendMessageAsync(MimeMessage emailMessage)
    {
        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port ?? default);
        await smtpClient.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await smtpClient.SendAsync(emailMessage);
        await smtpClient.DisconnectAsync(true);
    }
}
