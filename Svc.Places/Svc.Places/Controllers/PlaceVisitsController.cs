using Microsoft.Extensions.Logging;
using Svc.Places.Models.Criterias;
using Svc.Places.Models.Data;
using Nano.App.Api.Controllers;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;

namespace Svc.Places.Controllers;

/// <inheritdoc />
public class PlaceVisitsController(ILogger<PlaceVisitsController> logger, IRepository repository, IEventing eventing)
    : BaseEntityReadOnlyController<PlaceVisit, PlaceVisitQueryCriteria>(logger, repository, eventing);