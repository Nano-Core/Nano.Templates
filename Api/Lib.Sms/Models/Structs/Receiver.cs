using System.ComponentModel.DataAnnotations;

namespace Lib.Sms.Models.Structs;

/// <summary>
/// Receiver.
/// </summary>
public class Receiver
{
    /// <summary>
    /// Phone NUmber.
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
}