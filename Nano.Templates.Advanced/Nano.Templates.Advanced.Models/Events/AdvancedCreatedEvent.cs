using System;

namespace Nano.Templates.Advanced.Models.Events
{
    /// <summary>
    /// Advanced Created Event.
    /// </summary>
    public class AdvancedCreatedEvent
    {
        /// <summary>
        /// Id.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Property One.
        /// </summary>
        public virtual string PropertyOne { get; set; }

        /// <summary>
        /// Property Two.
        /// </summary>
        public virtual string PropertyTwo { get; set; }
    }
}