using System.Threading;
using System.Threading.Tasks;
using Lib.Emailing.Models;

namespace Lib.Emailing.Interfaces;

/// <summary>
/// Emailing Service Interface.
/// </summary>
public interface IEmailingService
{
    /// <summary>
    /// Send Email Async.
    /// </summary>
    /// <param name="email">The <see cref="Email"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    Task SendEmailAsync(Email email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send Email Template Async.
    /// </summary>
    /// <param name="emailTemplate">The <see cref="EmailTemplate"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>Void.</returns>
    Task SendEmailTemplateAsync(EmailTemplate emailTemplate, CancellationToken cancellationToken = default);
}