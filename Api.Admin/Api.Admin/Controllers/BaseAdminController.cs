using Api.Admin.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;

namespace Api.Admin.Controllers;

/// <inheritdoc />
[Authorize(Policy = CustomAuthorizationPolicies.SYSTEM_ADMIN)]
public abstract class BaseAdminController(ILogger<BaseAdminController> logger)
    : BaseController(logger);