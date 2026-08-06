using System;
using Svc.Accounts.Models.Data;

namespace Api.Public.Responses.Accounts;

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
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Phone Preifx.
    /// </summary>
    public string PhonePreifx { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="country">The country.</param>
    public CountryResponse(Country? country)
    {
        ArgumentNullException.ThrowIfNull(country);

        this.Id = country.Id;
        this.Name = country.Name;
        this.Code = country.Code;
        this.PhonePreifx = country.PhonePrefix;
    }
}