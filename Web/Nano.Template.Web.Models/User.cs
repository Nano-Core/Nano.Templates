using System.ComponentModel.DataAnnotations;
using Nano.Models;

namespace Nano.Template.Web.Models;

/// <summary>
/// User.
/// </summary>
public class User : DefaultEntityUser
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string Name { get; set; }
}