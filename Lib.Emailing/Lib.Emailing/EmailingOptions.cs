using System.ComponentModel.DataAnnotations;

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
    [Required]
    public virtual string ApiKey { get; set; } = null!;

    /// <summary>
    /// Sender Email.
    /// </summary>
    [Required]
    public virtual string SenderName { get; set; } = null!;

    /// <summary>
    /// Sender Email.
    /// </summary>
    [Required]
    public virtual string SenderEmailAddress { get; set; } = null!;
}