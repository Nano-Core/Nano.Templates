namespace Lib.Emailing;

/// <summary>
/// Emailing Options.
/// </summary>
public class EmailingOptions
{
    /// <summary>
    /// Section Name.
    /// </summary>
    internal static string SectionName => "Emailing";

    /// <summary>
    /// Api Key.
    /// </summary>
    public virtual string ApiKey { get; set; }

    /// <summary>
    /// Sender Email.
    /// </summary>
    public virtual string SenderName { get; set; }

    /// <summary>
    /// Sender Email.
    /// </summary>
    public virtual string SenderEmailAddress { get; set; }
}