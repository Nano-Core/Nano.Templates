using Svc.Accounts.Models.Data.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Sign Up Request.
/// </summary>
public class SignUpRequest
{
    /// <summary>
    /// First Name.
    /// </summary>
    [Required]
    public virtual required Guid TenantId { get; set; }

    /// <summary>
    /// First Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string FirstName { get; set; }

    /// <summary>
    /// Last Name.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string LastName { get; set; }

    /// <summary>
    /// Email Address.
    /// </summary>
    [Required]
    [EmailAddress]
    public virtual required string EmailAddress { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(128)]
    public virtual required string Password { get; set; }

    /// <summary>
    /// Date of Birth.
    /// </summary>
    [Required]
    public virtual required DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    [Required]
    public virtual required AddressRequest Address { get; set; }

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
}