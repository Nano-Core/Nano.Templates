using System.ComponentModel.DataAnnotations;
using Nano.Eventing.Attributes;
using Nano.Models;

namespace Nano.Templates.Simple.Models
{
    /// <summary>
    /// Simple Entity.
    /// </summary>
    [Publish]
    [Subscribe]
    public class SimpleEntity : DefaultEntity
    {
        /// <summary>
        /// Property One.
        /// </summary>
        [Required]
        public virtual string PropertyOne { get; set; }

        /// <summary>
        /// Property Two.
        /// </summary>
        public virtual string PropertyTwo { get; set; }

        /// <summary>
        /// Nested Example Entity.
        /// </summary>
        public virtual SimpleEntity NestedSimpleEntity { get; set; }
    }
}