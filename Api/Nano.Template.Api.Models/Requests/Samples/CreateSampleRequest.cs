using System.ComponentModel.DataAnnotations;

namespace Nano.Template.Api.Models.Requests.Samples;

/// <summary>
/// Create Sample Request.
/// </summary>
public class CreateSampleRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public virtual string Name { get; set; }
}