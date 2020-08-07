using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Repository.Interfaces;
using Nano.Template.Web.Models;
using Nano.Template.Web.Models.Criterias;
using Nano.Web.Controllers;

namespace Nano.Template.Web.Controllers
{
    /// <inheritdoc />
    public class SamplesController : DefaultController<Sample, SampleQueryCriteria>
    {
        /// <inheritdoc />
        public SamplesController(ILogger logger, IRepository repository, IEventing eventing)
            : base(logger, repository, eventing)
        {

        }
    }
}