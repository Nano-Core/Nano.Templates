using System;
using System.Threading;
using System.Threading.Tasks;
using Lib.Sms.Interfaces;

namespace Lib.Sms;

/// <summary>
/// Emailing Service.
/// </summary>
/// <param name="smsOptions">The <see cref="Options"/>.</param>
public class SmsService(SmsOptions smsOptions) : ISmsService
{
    /// <summary>
    /// Options.
    /// </summary>
    protected SmsOptions Options { get; } = smsOptions ?? throw new ArgumentNullException(nameof(smsOptions));

    /// <inheritdoc />
    public virtual async Task SendSmsAsync(Models.Sms sms, CancellationToken cancellationToken = default)
    {
        if (sms == null)
            throw new ArgumentNullException(nameof(sms));

        await Task.CompletedTask;

        throw new NotImplementedException();
    }
}