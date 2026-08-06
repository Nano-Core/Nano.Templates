using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;

namespace Svc.Accounts.Models.Api.Requests;

/// <summary>
/// Get All Tenants Request.
/// </summary>
[GetAction("all")]
public class GetAllTenantsRequest : BaseRequest;