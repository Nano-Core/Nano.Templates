using Nano.Models;
using Nano.Models.Types;

namespace Nano.Templates.Advanced.Models
{
    /// <inheritdoc />
    public class AdvancedEntity : DefaultEntity
    {
        /// <summary>
        /// Angle.
        /// </summary>
        public virtual Angle Angle { get; set; }

        /// <summary>
        /// Authentication Credential.
        /// </summary>
        public virtual AuthenticationCredential AuthenticationCredential { get; set; }

        /// <summary>
        /// Duration.
        /// </summary>
        public virtual Duration Duration { get; set; }

        /// <summary>
        /// Distance.
        /// </summary>
        public virtual Distance Distance { get; set; }

        /// <summary>
        /// Distance.
        /// </summary>
        public virtual EmailAddress EmailAddress { get; set; }

        /// <summary>
        /// Location.
        /// </summary>
        public virtual Location Location { get; set; }

        /// <summary>
        /// Percentage.
        /// </summary>
        public virtual Percentage Percentage { get; set; }

        /// <summary>
        /// Period.
        /// </summary>
        public virtual Period Period { get; set; }

        /// <summary>
        /// Phone Number.
        /// </summary>
        public virtual PhoneNumber PhoneNumber { get; set; }
    }
}