namespace Lib.Sms;

/// <summary>
/// Emailing Options.
/// </summary>
public class SmsOptions
{
    /// <summary>
    /// Section Name.
    /// </summary>
    internal static string SectionName => "Sms";

    /// <summary>
    /// Api Key.
    /// </summary>
    public virtual string ApiKey { get; set; }

    /// <summary>
    /// Phone Number.
    /// </summary>
    public virtual string SenderPhoneNumber { get; set; }
}