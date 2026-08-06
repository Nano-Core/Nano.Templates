using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Requests.Accounts;

/// <summary>
/// Address Request.
/// </summary>
public class AddressRequest
{
    /// <summary>
    /// Country Id.
    /// </summary>
    [Required]
    public virtual required Guid CountryId { get; set; }

    /// <summary>
    /// City.
    /// </summary>
    [Required]
    public virtual required CityRequest City { get; set; }

    /// <summary>
    /// Street Name.
    /// </summary>
    [Required]
    [MaxLength(512)]
    public virtual required string StreetName { get; set; }

    /// <summary>
    /// House Number.
    /// </summary>
    [MaxLength(16)]
    public virtual string? HouseNumber { get; set; }
}