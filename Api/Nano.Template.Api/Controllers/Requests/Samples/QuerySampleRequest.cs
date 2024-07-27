using DynamicExpression.Entities;

namespace Nano.Template.Api.Controllers.Requests.Samples;

/// <summary>
/// Query Sample Request.
/// </summary>
public class QuerySampleRequest
{
    /// <summary>
    /// Name.
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// Order.
    /// </summary>
    public virtual Ordering Order { get; set; } = new();

    /// <summary>
    /// Pagination.
    /// </summary>
    public virtual Pagination Paging { get; set; } = new();
}