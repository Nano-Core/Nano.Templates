using System.Collections.Generic;

namespace Lib.Emailing.Models;

/// <summary>
/// Email Template.
/// </summary>
public class EmailTemplate : BaseEmail
{
    /// <summary>
    /// Template Id.
    /// </summary>
    public virtual string TemplateId { get; set; } = null!;

    /// <summary>
    /// Data.
    /// </summary>
    public virtual IDictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
}