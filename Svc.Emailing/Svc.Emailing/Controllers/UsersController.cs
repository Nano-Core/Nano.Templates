using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Emailing.Models.Criterias;
using Svc.Emailing.Models.Data;

namespace Svc.Emailing.Controllers;

/// <inheritdoc />
public class UsersController(ILogger<UsersController> logger, IRepository repository, IEventing eventing)
    : BaseEntityCreatableController<User, UserQueryCriteria>(logger, repository, eventing);