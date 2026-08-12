using Svc.Places.Models.Criterias.Types;
using System.ComponentModel.DataAnnotations;

namespace Api.Public.Responses.Places;

/// <summary>
/// Within Request.
/// </summary>
public class WithinRequest
{
    /// <summary>
    /// Viewport.
    /// </summary>
    [Required]
    public virtual required Viewport Viewport { get; set; }
}