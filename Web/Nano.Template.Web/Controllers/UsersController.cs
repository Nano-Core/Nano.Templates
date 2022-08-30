using Microsoft.Extensions.Logging;
using Nano.Eventing.Interfaces;
using Nano.Repository.Interfaces;
using Nano.Security;
using Nano.Template.Web.Models;
using Nano.Template.Web.Models.Criterias;
using Nano.Web.Controllers;

namespace Nano.Template.Web.Controllers;

/// <inheritdoc />
public class UsersController : DefaultIdentityController<User, UserQueryCriteria>
{
    /// <inheritdoc />
    public UsersController(ILogger logger, IRepository repository, IEventing eventing, DefaultIdentityManager identityManager)
        : base(logger, repository, eventing, identityManager)
    {

    }
}