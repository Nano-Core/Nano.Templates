using System;
using Nano.Models.Types;
using Nano.Template.Web.Models;

namespace Nano.Template.Api.Models.Responses.Profiles;

/// <summary>
/// Get User Response.
/// </summary>
public class GetUserResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Email Address.
    /// </summary>
    public EmailAddress EmailAddress { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUserResponse()
    {

    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="user">The <see cref="User"/>.</param>
    public GetUserResponse(User user)
        : this()
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        this.Id = user.Id;
        this.Name = user.Name;
        this.EmailAddress = new EmailAddress
        {
            Email = user.IdentityUser.Email
        };
    }
}