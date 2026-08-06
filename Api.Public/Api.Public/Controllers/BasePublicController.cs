using Api.Public.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;

namespace Api.Public.Controllers;

/// <inheritdoc />
[Authorize(Policy = CustomAuthorizationPolicies.PUBLIC_USER)]
public abstract class BasePublicController(ILogger<BasePublicController> logger)
    : BaseController(logger);