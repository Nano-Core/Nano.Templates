using Api.Public.Responses.Places;
using Svc.Accounts.Models.Data.Enums;

namespace Api.Public.Responses.Accounts;

/// <summary>
/// Language Response.
/// </summary>
public class LanguageResponse : BaseTypeResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="language">The language.</param>
    public LanguageResponse(Language language)
        : base(language.ToString(), language.ToString())
    {
    }
}