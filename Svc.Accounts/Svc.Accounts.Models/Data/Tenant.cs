using Nano.Data.Abstractions.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Z.EntityFramework.Plus;

namespace Svc.Accounts.Models.Data;

/// <summary>
/// Tenant.
/// </summary>
public class Tenant : BaseEntity
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
    /// Users.
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = [];
}