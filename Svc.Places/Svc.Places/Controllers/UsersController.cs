using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Places.Models.Criterias;
using Svc.Places.Models.Data;

namespace Svc.Places.Controllers;

/// <inheritdoc />
public class UsersController(ILogger<UsersController> logger, IRepository repository, IEventing eventing)
    : BaseEntityCreatableController<User, UserQueryCriteria>(logger, repository, eventing);