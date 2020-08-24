using DynamicExpression.Entities;
using DynamicExpression.Interfaces;
using Z.BulkOperations;

namespace Nano.Template.Api.Models.Requests.Samples
{
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
        public virtual IOrdering Order { get; set; } = new Ordering();

        /// <summary>
        /// Pagination.
        /// </summary>
        public virtual IPagination Paging { get; set; } = new Pagination();
    }
}