using System.ComponentModel.DataAnnotations;

namespace Lib.Emailing.Models.Structs;

/// <summary>
/// Receiver.
/// </summary>
public class Receiver
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Email Address.
    /// </summary>
    [Required]
    public string EmailAddress { get; set; }
}