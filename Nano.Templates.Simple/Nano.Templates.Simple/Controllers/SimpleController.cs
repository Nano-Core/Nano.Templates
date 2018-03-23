using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Services.Interfaces;
using Nano.Templates.Simple.Models;
using Nano.Templates.Simple.Models.Criterias;
using Nano.Web.Controllers;

namespace Nano.Templates.Simple.Controllers
{
    /// <inheritdoc />
    public class SimpleController : DefaultController<SimpleEntity, SimpleQueryCriteria>
    {
        /// <inheritdoc />
        public SimpleController(ILogger logger, IService service, IEventing eventing)
            : base(logger, service, eventing)
        {
            
        }
    }
}