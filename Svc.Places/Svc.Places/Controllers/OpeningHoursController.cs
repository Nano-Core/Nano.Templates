using Microsoft.Extensions.Logging;
using Nano.App.Api.Controllers;
using Nano.Data.Abstractions;
using Nano.Eventing.Abstractions;
using Svc.Places.Models.Criterias;
using Svc.Places.Models.Data;

namespace Svc.Places.Controllers;

/// <inheritdoc />
public class OpeningHoursController(ILogger<OpeningHoursController> logger, IRepository repository, IEventing eventing)
    : BaseEntityController<OpeningHour, OpeningHourQueryCriteria>(logger, repository, eventing);