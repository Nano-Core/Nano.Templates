using System;

namespace Api.Public.Responses.Places;

/// <summary>
/// Base Type Response.
/// </summary>
public class BaseTypeResponse
{
    /// <summary>
    /// Type.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Display Name.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="displayName">The display name.</param>
    protected BaseTypeResponse(string type, string? displayName = null)
    {
        ArgumentNullException.ThrowIfNull(type);

        this.Type = type;
        this.DisplayName = displayName;
    }
}