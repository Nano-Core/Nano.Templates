using Microsoft.Extensions.Logging;
using Nano.Basic.Models;
using Nano.Basic.Models.Criterias;
using Nano.Eventing.Interfaces;
using Nano.Services.Interfaces;
using Nano.Web.Controllers;

namespace Nano.Basic.Controllers
{
    /// <inheritdoc />
    public class PersonsController : DefaultController<Person, PersonQueryCriteria>
    {
        /// <inheritdoc />
        public PersonsController(ILogger logger, IService service, IEventing eventing)
            : base(logger, service, eventing)
        {
            
        }
    }
}