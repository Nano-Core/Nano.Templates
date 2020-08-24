using System;
using Nano.Template.Web.Models;

namespace Nano.Template.Api.Models.Responses.Samples
{
    /// <summary>
    /// Get Sample Response.
    /// </summary>
    public class GetSampleResponse
    {
        /// <summary>
        /// Id.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Created At.
        /// </summary>
        public virtual DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sample">The <see cref="Sample"/>.</param>
        public GetSampleResponse(Sample sample)
        {
            if (sample == null) 
                throw new ArgumentNullException(nameof(sample));

            this.Id = sample.Id;
            this.CreatedAt = sample.CreatedAt;
            this.Name = sample.Name;
        }
    }
}