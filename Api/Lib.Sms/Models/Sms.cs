using System.ComponentModel.DataAnnotations;
using Lib.Sms.Models.Structs;

namespace Lib.Sms.Models;

/// <summary>
/// Sms. 
/// </summary>
public class Sms
{
    /// <summary>
    /// Receiver.
    /// </summary>
    [Required]
    public virtual Receiver Receiver { get; set; } = new();

    /// <summary>
    /// Text.
    /// </summary>
    public virtual string Text { get; set; }
}