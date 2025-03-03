﻿using System.ComponentModel.DataAnnotations;
using Lib.Sms.Models.Structs;

namespace Lib.Sms.Models;

/// <summary>
/// Message. 
/// </summary>
public class Message
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