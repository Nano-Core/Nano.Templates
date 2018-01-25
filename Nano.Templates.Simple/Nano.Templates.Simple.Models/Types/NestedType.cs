using Nano.Models.Types;

namespace Nano.Templates.Simple.Models.Types
{
    /// <summary>
    /// Nested Type.
    /// </summary>
    public class NestedType
    {
        /// <summary>
        /// Duration.
        /// </summary>
        public virtual Duration Duration { get; set; }

        /// <summary>
        /// Distance.
        /// </summary>
        public virtual Distance Distance { get; set; }
    }
}