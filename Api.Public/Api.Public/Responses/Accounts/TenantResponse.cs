using System;
using Svc.Accounts.Models.Data;

namespace Api.Public.Responses.Accounts;

/// <summary>
/// Tenant Response.
/// </summary>
public class TenantResponse
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
    /// Constructor.
    /// </summary>
    /// <param name="tenant">The tenant.</param>
    public TenantResponse(Tenant tenant)
    {
        ArgumentNullException.ThrowIfNull(tenant);

        this.Id = tenant.Id;
        this.Name = tenant.Name;
    }
}