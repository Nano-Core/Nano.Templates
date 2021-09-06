using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Web.Models
{
    /// <summary>
    /// Derived Sample 2.
    /// </summary>
    public class DerivedSample2 : Sample
    {
        /// <summary>
        /// Summary.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string Summary { get; set; }
    }
}