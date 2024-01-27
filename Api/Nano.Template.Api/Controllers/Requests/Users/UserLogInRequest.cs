using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Controllers.Requests.Users;

/// <summary>
/// User Log In Request.
/// </summary>
public class UserLogInRequest
{
    /// <summary>
    /// App Id.
    /// </summary>
    [MaxLength(256)]
    public virtual string AppId { get; set; }

    /// <summary>
    /// Username.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string Username { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string Password { get; set; }
}