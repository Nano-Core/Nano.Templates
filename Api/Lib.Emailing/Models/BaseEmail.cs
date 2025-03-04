using Lib.Emailing.Models.Structs;

namespace Lib.Emailing.Models;

/// <summary>
/// Base Email (abstract).
/// </summary>
public abstract class BaseEmail
{
    /// <summary>
    /// Receiver.
    /// </summary>
    public virtual Receiver Receiver { get; set; } = new();
}