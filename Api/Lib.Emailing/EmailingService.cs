using System;
using System.Threading;
using System.Threading.Tasks;
using Lib.Emailing.Interfaces;
using Lib.Emailing.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Lib.Emailing;

/// <summary>
/// Emailing Service.
/// </summary>
/// <param name="sendGridClient">The <see cref="ISendGridClient"/>.</param>
/// <param name="emailingOptions">The <see cref="Options"/>.</param>
public class EmailingService(ISendGridClient sendGridClient, EmailingOptions emailingOptions) : IEmailingService
{
    /// <summary>
    /// Client.
    /// </summary>
    protected ISendGridClient Client { get; } = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));

    /// <summary>
    /// Options.
    /// </summary>
    protected EmailingOptions Options { get; } = emailingOptions ?? throw new ArgumentNullException(nameof(emailingOptions));

    /// <inheritdoc />
    public virtual async Task SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));

        var message = new SendGridMessage();

        message
            .SetFrom(this.Options.SenderEmailAddress, this.Options.SenderName);

        message
            .AddTo(email.Receiver.EmailAddress, email.Receiver.Name);

        message
            .SetSubject(email.Subject);

        message
            .SetClickTracking(false, true);

        message.HtmlContent = email.HtmlBody;
        message.PlainTextContent = email.Body;

        await this.Client
            .SendEmailAsync(message, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task SendEmailTemplateAsync(EmailTemplate emailTemplate, CancellationToken cancellationToken = default)
    {
        if (emailTemplate == null)
            throw new ArgumentNullException(nameof(emailTemplate));

        var message = new SendGridMessage();

        message
            .SetFrom(this.Options.SenderEmailAddress, this.Options.SenderName);

        message
            .AddTo(emailTemplate.Receiver.EmailAddress, emailTemplate.Receiver.Name);

        message
            .SetClickTracking(false, true);

        message
            .SetTemplateId(emailTemplate.TemplateId);

        message
            .SetTemplateData(emailTemplate.Data);

        await this.Client
            .SendEmailAsync(message, cancellationToken);
    }
}