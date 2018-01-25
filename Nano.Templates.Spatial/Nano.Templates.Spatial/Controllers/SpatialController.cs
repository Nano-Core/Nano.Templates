using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Services.Interfaces;
using Nano.Templates.Spatial.Models;
using Nano.Templates.Spatial.Models.Criterias;
using Nano.Web.Controllers;

namespace Nano.Templates.Spatial.Controllers
{
    /// <inheritdoc />
    public class SpatialController : DefaultController<SpatialEntity, SpatialQueryCriteria>
    {
        /// <inheritdoc />
        public SpatialController(ILogger logger, IService service, IEventing eventing)
            : base(logger, service, eventing)
        {

        }
    }
}