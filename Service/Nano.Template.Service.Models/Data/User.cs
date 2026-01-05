using System.ComponentModel.DataAnnotations;
using Nano.Data.Abstractions.Annotations;
using Nano.Data.Abstractions.Eventing.Annotations;
using Nano.Data.Abstractions.Models;

namespace Nano.Template.Service.Models.Data;

/// <summary>
/// User.
/// </summary>
[Publish("Name", "IdentityUser.EmailConfirmed")]
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