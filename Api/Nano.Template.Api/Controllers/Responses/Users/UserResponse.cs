using System;
using Nano.Template.Web.Models.Data;

namespace Nano.Template.Api.Controllers.Responses.Users;

/// <summary>
/// Get User Response.
/// </summary>
public class UserResponse
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
    public string EmailAddress { get; set; }

    /// <summary>
    /// Phone Number.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="user">The <see cref="User"/>.</param>
    public UserResponse(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        this.Id = user.Id;
        this.Name = user.Name;
        this.EmailAddress = user.IdentityUser.Email;
        this.PhoneNumber = user.IdentityUser.PhoneNumber;
    }
}