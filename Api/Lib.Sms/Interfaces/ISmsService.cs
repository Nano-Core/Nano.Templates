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
    /// <param name="sms">The <see cref="Sms"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    Task SendSmsAsync(Models.Sms sms, CancellationToken cancellationToken = default);
}