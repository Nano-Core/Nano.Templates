using System;
using System.Threading;
using System.Threading.Tasks;
using Lib.Sms.Interfaces;
using Lib.Sms.Models;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Lib.Sms;

/// <summary>
/// Sms Service.
/// </summary>
/// <param name="smsOptions">The <see cref="SmsOptions"/>.</param>
/// <param name="twilioClient">The <see cref="ITwilioRestClient"/>.</param>
public class SmsService(SmsOptions smsOptions, ITwilioRestClient twilioClient) : ISmsService
{
    /// <summary>
    /// Options.
    /// </summary>
    protected virtual SmsOptions Options { get; } = smsOptions ?? throw new ArgumentNullException(nameof(smsOptions));

    /// <summary>
    /// Twilio Client.
    /// </summary>
    protected virtual ITwilioRestClient TwilioClient { get; set; } = twilioClient ?? throw new ArgumentNullException(nameof(twilioClient));

    /// <inheritdoc />
    public virtual async Task SendSmsAsync(Message message, CancellationToken cancellationToken = default)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var text = message.Text;
        var senderPhoneNumber = new PhoneNumber(this.Options.SenderPhoneNumber);
        var receiverPhoneNumber = new PhoneNumber(message.Receiver.PhoneNumber);

        await MessageResource.CreateAsync(body: text, from: senderPhoneNumber, to: receiverPhoneNumber, client: this.TwilioClient);
    }
}