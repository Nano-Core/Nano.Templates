using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Locations.Models.Criterias;
using Svc.Locations.Models.Data;

namespace Svc.Locations.Controllers;

/// <inheritdoc />
public class UsersController(ILogger<UsersController> logger, IRepository repository, IEventing eventing)
    : BaseEntityCreatableController<User, UserQueryCriteria>(logger, repository, eventing);