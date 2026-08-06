using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nano.Data.Abstractions.Models;
using Svc.Emailing.Models.Data.Enums;

namespace Svc.Emailing.Models.Data;

/// <summary>
/// Email.
/// </summary>
public class Email : BaseEntity
{
    /// <summary>
    /// User Id.
    /// </summary>
    [Required]
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// User.
    /// </summary>
    public virtual User? User { get; set; }

    /// <summary>
    /// Template Id.
    /// </summary>
    [Required]
    [DefaultValue(EmailType.None)]
    public virtual EmailType Type { get; set; } = EmailType.None;

    /// <summary>
    /// Data.
    /// </summary>
    [Required]
    public virtual IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
}