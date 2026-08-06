namespace Lib.Emailing.Models;

/// <summary>
/// Email.
/// </summary>
public class Email : BaseEmail
{
    /// <summary>
    /// Subject.
    /// </summary>
    public virtual string Subject { get; set; } = null!;

    /// <summary>
    /// Body (plain text).
    /// </summary>
    public virtual string Body { get; set; } = null!;

    /// <summary>
    /// Body (html).
    /// </summary>
    public virtual string HtmlBody { get; set; } = null!;
}