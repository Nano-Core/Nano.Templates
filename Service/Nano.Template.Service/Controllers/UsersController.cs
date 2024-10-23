using Microsoft.Extensions.Logging;
using Nano.Models.Eventing.Interfaces;
using Nano.Repository.Interfaces;
using Nano.Security;
using Nano.Template.Service.Models.Criterias;
using Nano.Template.Service.Models.Data;
using Nano.Web.Controllers;

namespace Nano.Template.Service.Controllers;

/// <inheritdoc />
public class UsersController : BaseDefaultIdentityController<User, UserQueryCriteria>
{
    /// <inheritdoc />
    public UsersController(ILogger logger, IRepository repository, IEventing eventing, DefaultIdentityManager identityManager)
        : base(logger, repository, eventing, identityManager)
    {
    }
}