using System.ComponentModel.DataAnnotations;
using Nano.Models;
using Nano.Models.Attributes;

namespace Nano.Template.Service.Models.Data;

/// <summary>
/// User.
/// </summary>
[UxException("Custom UX Error...", [nameof(Name), nameof(IsDeleted)])]
public class User : DefaultEntityUser
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string Name { get; set; }
}