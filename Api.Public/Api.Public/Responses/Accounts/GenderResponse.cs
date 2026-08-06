using Api.Public.Responses.Places;
using Svc.Accounts.Models.Data.Enums;

namespace Api.Public.Responses.Accounts;

/// <summary>
/// Gender Response.
/// </summary>
public class GenderResponse : BaseTypeResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="gender">The gender.</param>
    public GenderResponse(Gender gender)
        : base(gender.ToString(), gender.ToString())
    {
    }
}