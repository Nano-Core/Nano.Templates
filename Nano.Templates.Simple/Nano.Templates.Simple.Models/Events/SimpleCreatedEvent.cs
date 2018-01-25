using System;

namespace Nano.Templates.Simple.Models.Events
{
    /// <summary>
    /// Simple Created Event.
    /// </summary>
    public class SimpleCreatedEvent
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