using System.ComponentModel.DataAnnotations;
using Z.EntityFramework.Plus;

namespace Svc.Accounts.Models.Data.Types;

/// <summary>
/// City.
/// </summary>
public class City
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string Name
    {
        get;
        set
        {
            field = value;
            this.NameNormalized = value.ToUpper();
        }
    } = null!;

    /// <summary>
    /// Name Normalized.
    /// </summary>
    [Required]
    [MaxLength(128)]
    [AuditExclude]
    public virtual string NameNormalized { get; internal set; } = null!;

    /// <summary>
    /// Zip Code.
    /// </summary>
    [Required]
    [MaxLength(32)]
    public virtual string ZipCode { get; set; } = null!;
}