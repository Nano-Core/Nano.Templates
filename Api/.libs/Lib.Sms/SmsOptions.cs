namespace Lib.Sms;

/// <summary>
/// Sms Options.
/// </summary>
public class SmsOptions
{
    /// <summary>
    /// Section Name.
    /// </summary>
    internal static string SectionName => "Sms";

    /// <summary>
    /// Account Id.
    /// </summary>
    public virtual string AccountId { get; set; }

    /// <summary>
    /// Auth Token.
    /// </summary>
    public virtual string AuthToken { get; set; }

    /// <summary>
    /// Sender Phone Number.
    /// </summary>
    public virtual string SenderPhoneNumber { get; set; }
}