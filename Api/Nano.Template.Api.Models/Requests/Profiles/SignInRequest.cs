using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Models.Requests.Profiles
{
    /// <summary>
    /// Log-In Request.
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// App Id.
        /// </summary>
        [MaxLength(256)]
        public virtual string AppId { get; set; }

        /// <summary>
        /// Username (email).
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
}