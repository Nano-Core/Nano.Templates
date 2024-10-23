using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nano.Models;
using Nano.Models.Attributes;

namespace Nano.Template.Service.Models.Data;

/// <summary>
/// Sample.
/// </summary>
[UxException("UX Sample Exception", [nameof(Name)])]
public class Sample : DefaultEntity
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string Name { get; set; }

    /// <summary>
    /// Has Name.
    /// </summary>
    [NotMapped]
    public virtual bool HasName => this.Name != null;
}