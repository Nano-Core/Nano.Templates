using Lib.Emailing.Interfaces;
using Lib.Emailing.Models;
using Resend;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Emailing;

/// <summary>
/// Resend Emailing Service.
/// </summary>
/// <param name="resendClient ">The <see cref="IResend"/>.</param>
/// <param name="emailingOptions">The <see cref="EmailingOptions"/>.</param>
public class ResendEmailingService(IResend resendClient, EmailingOptions emailingOptions) : IEmailingService
{
    /// <inheritdoc />
    public virtual async Task SendEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(email);

        await resendClient
            .EmailSendAsync(new EmailMessage
            {
                From = new EmailAddress
                {
                    Email = emailingOptions.SenderEmailAddress,
                    DisplayName = emailingOptions.SenderName
                },
                To =
                [
                    new EmailAddress
                    {
                        Email = email.Receiver.EmailAddress,
                        DisplayName = email.Receiver.Name
                    }
                ],
                Subject = email.Subject,
                TextBody = email.Body,
                HtmlBody = email.HtmlBody
            }, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task SendEmailTemplateAsync(EmailTemplate emailTemplate, CancellationToken cancellationToken = default)
    {
        if (emailTemplate == null)
            throw new ArgumentNullException(nameof(emailTemplate));

        await resendClient
            .EmailSendAsync(new EmailMessage
            {
                From = new EmailAddress
                {
                    Email = emailingOptions.SenderEmailAddress,
                    DisplayName = emailingOptions.SenderName
                },
                To =
                [
                    new EmailAddress
                    {
                        Email = emailTemplate.Receiver.EmailAddress,
                        DisplayName = emailTemplate.Receiver.Name
                    }
                ],
                Template = new EmailMessageTemplate
                {
                    TemplateId = emailTemplate.TemplateId,
                    Variables = emailTemplate.Data
                        .ToDictionary(x => x.Key, x => x.Value)
                }
            }, cancellationToken);
    }
}