using System.ComponentModel.DataAnnotations;
using Nano.Models;

namespace Nano.Templates.Spatial.Models
{
    /// <inheritdoc />
    public class SpatialEntity : DefaultEntitySpatial
    {
        /// <summary>
        /// Property One.
        /// </summary>
        [Required]
        public virtual string PropertyOne { get; set; }
    }
}