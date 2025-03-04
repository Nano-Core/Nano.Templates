using System.Threading;
using System.Threading.Tasks;
using Lib.Sms.Models;

namespace Lib.Sms.Interfaces;

/// <summary>
/// Sms Service Interface.
/// </summary>
public interface ISmsService
{
    /// <summary>
    /// Send Sms Async.
    /// </summary>
    /// <param name="message">The <see cref="Message"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    Task SendSmsAsync(Message message, CancellationToken cancellationToken = default);
}