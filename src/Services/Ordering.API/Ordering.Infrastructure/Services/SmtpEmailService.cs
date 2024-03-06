using Contracts.Configurations;
using Contracts.Services;
using MailKit.Net.Smtp;
using MimeKit;
using Ordering.Infrastructure.Configurations;
using Serilog;
using Shared.Services.Email;

namespace Ordering.Infrastructure.Services;

public class SmtpEmailService:ISmtpEmailService
{

    private readonly ILogger _logger;
    private readonly SmtpEmailSetting _settings;
    private readonly SmtpClient _smtpClient;
    public SmtpEmailService(ILogger logger, SmtpEmailSetting settings)
    {
        _logger = logger?? throw new NullReferenceException( nameof(logger));
        _settings = settings ?? throw new NullReferenceException( nameof(settings));
        _smtpClient =  new SmtpClient();
    }
    public async Task SendEmailAsync(MailRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var emailMessage = new MimeMessage
        {
            Sender = new MailboxAddress(_settings.DisplayName, _settings.From ?? _settings.From),
            Subject = request.Subject,
            Body = new BodyBuilder
            {
                HtmlBody = request.Body
            }.ToMessageBody()
        };
        if (request.ToAddresses.Any())
        {
            foreach (var toAddress in request.ToAddresses)
            {
                emailMessage.To.Add( MailboxAddress.Parse(toAddress));
            }
        }
        else
        {
            var toAddress = request.ToAddress;
            emailMessage.To.Add( MailboxAddress.Parse(toAddress));
        }

        try
        {
            await _smtpClient.ConnectAsync(_settings.SMTPServer, _settings.Port, _settings.UseSsl, cancellationToken);
            await _smtpClient.AuthenticateAsync(_settings.UserName, _settings.Password, cancellationToken);
            await _smtpClient.SendAsync(emailMessage, cancellationToken);
            await _smtpClient.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            throw;
        }
        finally
        {
            await _smtpClient.DisconnectAsync(true, cancellationToken);
            _smtpClient.Dispose();
        }
    }
}