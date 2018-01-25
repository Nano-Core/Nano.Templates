using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Services.Interfaces;
using Nano.Templates.Advanced.Models;
using Nano.Templates.Advanced.Models.Criterias;
using Nano.Web.Controllers;

namespace Nano.Templates.Advanced.Controllers
{
    /// <inheritdoc />
    public class AdvancedNestedController : DefaultController<AdvancedNestedEntity, AdvancedNestedQueryCriteria>
    {
        /// <inheritdoc />
        public AdvancedNestedController(ILogger logger, IService service, IEventing eventing)
            : base(logger, service, eventing)
        {

        }
    }
}