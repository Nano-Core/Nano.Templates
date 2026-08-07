using Nano.Data.Abstractions.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Nano.Data.Abstractions.Annotations;
using Svc.Accounts.Models.Data.Types;
using Z.EntityFramework.Plus;

namespace Svc.Accounts.Models.Data;

/// <summary>
/// Address.
/// </summary>
public class Address : BaseEntity
{
    /// <summary>
    /// Country Id.
    /// </summary>
    [Required]
    public virtual Guid CountryId { get; set; }

    /// <summary>
    /// Country.
    /// </summary>
    [Include]
    public virtual Country? Country { get; set; }

    /// <summary>
    /// City.
    /// </summary>
    public virtual City City { get; set; } = new();

    /// <summary>
    /// Street Name.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public virtual string StreetName
    {
        get;
        set
        {
            field = value;
            this.StreetNameNormalized = value.ToUpper();
        }
    } = null!;

    /// <summary>
    /// Street Name Normalized.
    /// </summary>
    [Required]
    [MaxLength(512)]
    [AuditExclude]
    public virtual string StreetNameNormalized { get; internal set; } = null!;

    /// <summary>
    /// House Number.
    /// </summary>
    [MaxLength(16)]
    public virtual string? HouseNumber { get; set; }
}