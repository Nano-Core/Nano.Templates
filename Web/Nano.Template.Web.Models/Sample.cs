using System.ComponentModel.DataAnnotations;
using Nano.Models;

namespace Nano.Template.Web.Models
{
    /// <summary>
    /// Sample.
    /// </summary>
    public class Sample : DefaultEntity
    {
        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string Name { get; set; }
    }
}
