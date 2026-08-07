using System;
using Svc.Accounts.Models.Data;

namespace Api.Admin.Responses;

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
    /// Created At.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="tenant">The <see cref="Tenant"/>.</param>
    public TenantResponse(Tenant tenant)
    {
        ArgumentNullException.ThrowIfNull(tenant);

        this.Id = tenant.Id;
        this.Name = tenant.Name;
        this.CreatedAt = tenant.CreatedAt;
    }
}