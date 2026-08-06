using System;
using Svc.Accounts.Models.Data;

namespace Api.Public.Responses.Accounts;

/// <summary>
/// Address Response.
/// </summary>
public class AddressResponse
{
    /// <summary>
    /// Country.
    /// </summary>
    public CountryResponse Country { get; set; }

    /// <summary>
    /// City.
    /// </summary>
    public CityResponse City { get; set; }

    /// <summary>
    /// Street Name.
    /// </summary>
    public string StreetName { get; set; }

    /// <summary>
    /// House Number .
    /// </summary>
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="address">The <see cref="Address"/>.</param>
    public AddressResponse(Address? address)
    {
        ArgumentNullException.ThrowIfNull(address);

        this.Country = new CountryResponse(address.Country);
        this.City = new CityResponse(address.City);
        this.StreetName = address.StreetName;
        this.HouseNumber = address.HouseNumber;
    }
}