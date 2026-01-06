using Microsoft.Extensions.Logging;
using Nano.App.Web.Controllers;
using Nano.Data.Abstractions;
using Nano.Data.Abstractions.Identity;
using Nano.Data.Abstractions.Identity.Authentication;
using Nano.Template.Service.Models.Criterias;
using Nano.Template.Service.Models.Data;
using Nano.Eventing.Abstractions;

namespace Nano.Template.Service.Controllers;

/// <inheritdoc />
public class UsersController : DefaultIdentityController<User, UserQueryCriteria>
{
    /// <inheritdoc />
    public UsersController(ILogger logger, IRepository repository, IEventing eventing, IIdentityRepository identityRepository, IAuthExternalRepository authExternalRepository = null)
        : base(logger, repository, eventing, identityRepository, authExternalRepository)
    {
    }
}