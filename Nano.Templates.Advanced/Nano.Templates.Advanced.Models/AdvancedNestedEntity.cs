using Nano.Models;
using Nano.Templates.Advanced.Models.Types;

namespace Nano.Templates.Advanced.Models
{
    /// <inheritdoc />
    public class AdvancedNestedEntity : DefaultEntity
    {
        /// <summary>
        /// Nested Type.
        /// </summary>
        public virtual NestedType Nested { get; set; }
    }
}