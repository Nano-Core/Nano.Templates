namespace Lib.Emailing.Models.Structs;

/// <summary>
/// Receiver.
/// </summary>
public class Receiver
{
    /// <summary>
    /// Name.
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// Email Address.
    /// </summary>
    public virtual string EmailAddress { get; set; }
}