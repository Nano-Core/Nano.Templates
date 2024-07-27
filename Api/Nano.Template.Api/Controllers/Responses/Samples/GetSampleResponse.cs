using System;
using Nano.Template.Web.Models.Data;

namespace Nano.Template.Api.Controllers.Responses.Samples;

/// <summary>
/// Get Sample Response.
/// </summary>
public class GetSampleResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Created At.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

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