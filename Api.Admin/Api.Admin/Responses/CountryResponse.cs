using System;
using Svc.Accounts.Models.Data;

namespace Api.Admin.Responses;

/// <summary>
/// Country Response.
/// </summary>
public class CountryResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Code.
    /// ISO 3166-1 alpha-2.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Phone Prefix.
    /// </summary>
    public string PhonePrefix { get; set; }

    /// <summary>
    /// Created At.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="country">The <see cref="Country"/>.</param>
    public CountryResponse(Country country)
    {
        ArgumentNullException.ThrowIfNull(country);

        this.Id = country.Id;
        this.Name = country.Name;
        this.Code = country.Code;
        this.PhonePrefix = country.PhonePrefix;
        this.CreatedAt = country.CreatedAt;
    }
}