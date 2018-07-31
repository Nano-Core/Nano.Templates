using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nano.Basic.Models.Enums;
using Nano.Eventing.Attributes;
using Nano.Models;
using Nano.Models.Types;

namespace Nano.Basic.Models
{
    /// <inheritdoc />
    [Publish]
    [Subscribe]
    public class Person : DefaultEntity
    {
        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        [Required]
        [DefaultValue(Gender.Undefined)]
        public virtual Gender Gender { get; set; } = Gender.Undefined;

        /// <summary>
        /// Is Friendly.
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public virtual bool IsFriendly { get; set; } = true;

        /// <summary>
        /// Phone Number.
        /// </summary>
        public virtual PhoneNumber PhoneNumber { get; set; } = new PhoneNumber();

        /// <summary>
        /// Email Address.
        /// </summary>
        public virtual EmailAddress EmailAddress { get; set; } = new EmailAddress();
    }
}