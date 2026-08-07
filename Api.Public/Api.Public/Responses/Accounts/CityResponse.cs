using System;
using Svc.Accounts.Models.Data.Types;

namespace Api.Public.Responses.Accounts;

/// <summary>
/// City Response.
/// </summary>
public class CityResponse
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Zip Code.
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="city">The <see cref="City"/>.</param>
    public CityResponse(City? city)
    {
        ArgumentNullException.ThrowIfNull(city);

        this.Name = city.Name;
        this.ZipCode = city.ZipCode;
    }
}