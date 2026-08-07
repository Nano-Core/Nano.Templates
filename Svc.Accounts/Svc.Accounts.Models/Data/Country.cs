using Nano.Data.Abstractions.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Z.EntityFramework.Plus;

namespace Svc.Accounts.Models.Data;

/// <summary>
/// Country.
/// </summary>
public class Country : BaseEntity
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
    /// Code.
    /// ISO 3166-1 alpha-2.
    /// </summary>
    [Required]
    [MaxLength(5)]
    public virtual string Code { get; set; } = null!;

    /// <summary>
    /// Phone Prefix.
    /// </summary>
    [Required]
    [MaxLength(5)]
    public virtual string PhonePrefix { get; set; } = null!;

    /// <summary>
    /// Addresses.
    /// </summary>
    public virtual ICollection<Address> Addresses { get; set; } = [];
}