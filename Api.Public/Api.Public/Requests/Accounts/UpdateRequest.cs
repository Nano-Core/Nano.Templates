using Svc.Accounts.Models.Data.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Update Request.
/// </summary>
public class UpdateRequest
{
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
    public virtual required Gender Gender { get; set; } = Gender.Male;

    /// <summary>
    /// Language.
    /// </summary>
    [Required]
    [DefaultValue(Language.English)]
    public virtual required Language Language { get; set; } = Language.English;
}