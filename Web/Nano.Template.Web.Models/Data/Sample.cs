using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nano.Models;
using Nano.Models.Attributes;

namespace Nano.Template.Web.Models.Data;

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

    /// <summary>
    /// Has Name.
    /// </summary>
    [NotMapped]
    public virtual bool HasName => this.Name != null;

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public virtual Guid NestedId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Include]
    public virtual Nested Nested { get; set; }
}

/// <summary>
/// 
/// </summary>
public class Nested : DefaultEntity
{
    /// <summary>
    /// 
    /// </summary>
    public virtual IEnumerable<Sample> Samples { get; set; }
}
