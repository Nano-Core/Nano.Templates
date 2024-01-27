using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Web.Models.Data;

/// <summary>
/// Derived Sample 1.
/// </summary>
public class DerivedSample1 : Sample
{
    /// <summary>
    /// Description.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string Description { get; set; }
}