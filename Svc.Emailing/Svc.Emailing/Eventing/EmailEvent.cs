using Svc.Emailing.Models.Data;

namespace Svc.Emailing.Eventing;

/// <summary>
/// Email Event.
/// </summary>
public class EmailEvent
{
    /// <summary>
    /// Email.
    /// </summary>
    public virtual Email Email { get; set; } = null!;
}