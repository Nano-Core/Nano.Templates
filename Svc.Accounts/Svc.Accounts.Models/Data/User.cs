using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nano.Data.Abstractions.Annotations;
using Nano.Data.Abstractions.Eventing.Annotations;
using Nano.Data.Abstractions.Models;
using Svc.Accounts.Models.Data.Enums;
using Svc.Accounts.Models.Extensions;
using Z.EntityFramework.Plus;

namespace Svc.Accounts.Models.Data;

/// <summary>
/// User.
/// </summary>
[Publish(
    nameof(TenantId),
    nameof(FirstName),
    nameof(LastName),
    nameof(FullName),
    nameof(Language),
    $"{nameof(IdentityUser)}.{nameof(IdentityUser.IsActive)}",
    $"{nameof(IdentityUser)}.{nameof(IdentityUser.Email)}",
    $"{nameof(IdentityUser)}.{nameof(IdentityUser.PhoneNumber)}")]
public class User : BaseEntityUser
{
    private string firstName = null!;
    private string lastName = null!;

    /// <summary>
    /// Tenant Id.
    /// </summary>
    [Required]
    public virtual Guid TenantId { get; set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public virtual Tenant? Tenant { get; set; }

    /// <summary>
    /// First Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string FirstName
    {
        get => this.firstName;
        set
        {
            this.firstName = value;
            this.FullName = $"{this.firstName} {this.lastName}";
        }
    }

    /// <summary>
    /// Last Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual string LastName
    {
        get => this.lastName;
        set
        {
            this.lastName = value;
            this.FullName = $"{this.firstName} {this.lastName}";
        }
    }

    /// <summary>
    /// Full Name.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string FullName
    {
        get;
        set
        {
            field = value;
            this.FullNameNormalized = value.ToUpper();
        }
    } = null!;

    /// <summary>
    /// Full Name Normalized.
    /// </summary>
    [Required]
    [MaxLength(256)]
    [AuditExclude]
    public virtual string FullNameNormalized { get; internal set; } = null!;

    /// <summary>
    /// Address Id.
    /// </summary>
    public virtual Guid? AddressId { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    [Include]
    public virtual Address? Address { get; set; }

    /// <summary>
    /// Date of Birth.
    /// </summary>
    [Required]
    public virtual DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Age.
    /// </summary>
    [NotMapped]
    public virtual int Age => this.DateOfBirth.GetAge();

    /// <summary>
    /// Gender.
    /// </summary>
    [Required]
    [DefaultValue(Gender.Male)]
    public virtual Gender Gender { get; set; } = Gender.Male;

    /// <summary>
    /// Language.
    /// </summary>
    [Required]
    [DefaultValue(Language.English)]
    public virtual Language Language { get; set; } = Language.English;

    /// <summary>
    /// Profile Picture Extension
    /// </summary>
    [MaxLength(32)]
    public virtual string? UserPictureExtension { get; set; }
}