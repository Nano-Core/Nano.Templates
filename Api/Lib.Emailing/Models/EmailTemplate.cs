namespace Lib.Emailing.Models;

/// <summary>
/// Email Template.
/// </summary>
public class EmailTemplate : BaseEmail
{
    /// <summary>
    /// Template Id.
    /// </summary>
    public virtual string TemplateId { get; set; }

    /// <summary>
    /// Data.
    /// </summary>
    public virtual object Data { get; set; } = new();
}