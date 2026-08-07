using Nano.App.ApiClient.Annotations.Actions;
using Nano.App.ApiClient.Requests;

namespace Svc.Accounts.Models.Api.Requests;

/// <summary>
/// User New Sign-Ups Request.
/// </summary>
[GetAction("new-signups")]
public class UserNewSignUpsRequest : BaseRequest;