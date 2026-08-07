using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano.App.Console.Workers;
using Svc.Accounts.Models.Api;
using Svc.Emailing.Models.Api;
using Svc.Emailing.Models.Data;
using Svc.Emailing.Models.Data.Enums;

namespace Cmd.SignUps.Workers;

/// <inheritdoc/>
public class NewUserSignUpWorker(ILogger<NewUserSignUpWorker> logger, AccountsApi accountsApi, EmailingApi emailingApi) 
    : BaseWorker(logger)
{
    /// <inheritdoc />
    public override async Task OnStartAsync(CancellationToken cancellationToken = default)
    {
        var users = await accountsApi
            .GetNewSignUpsAsync(cancellationToken);

        foreach (var user in users)
        {
            await emailingApi
                .SendEmailAsync(new Email
                {
                    UserId = user.Id,
                    Type = EmailType.Welcome,
                    Data =
                    {
                        { "Name", user.FullName }
                    }
                }, cancellationToken);
        }
    }
}