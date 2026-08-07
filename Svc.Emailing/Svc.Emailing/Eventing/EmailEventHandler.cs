using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lib.Emailing.Interfaces;
using Lib.Emailing.Models;
using Lib.Emailing.Models.Structs;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Emailing.Eventing.Extensions;
using Svc.Emailing.Models.Data;

namespace Svc.Emailing.Eventing;

/// <inheritdoc />
public class EmailEventHandler(IRepository repository, IEmailingService emailingService) : BaseEventHandler<EmailEvent>
{
    /// <inheritdoc />
    public override async Task CallbackAsync(EmailEvent @event, bool isRedelivered, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(@event);

        await Task.Delay(2000, cancellationToken); 

        var user = await repository
            .GetAsync<User>(@event.Email.UserId, cancellationToken);

        if (user == null)
        {
            throw new NullReferenceException(nameof(user));
        }

        var templateId = @event.Email.Type
            .GetEmailTemplate();

        await emailingService
            .SendEmailTemplateAsync(new EmailTemplate
            {
                TemplateId = templateId,
                Receiver = new Receiver
                {
                    Name = user.FullName,
                    EmailAddress = user.Email
                },
                Data = @event.Email.Data
                    .ToDictionary(x => x.Key, object (x) => x.Value)
            }, cancellationToken);

        await repository
            .AddAsync(@event.Email, cancellationToken);
    }
}