using Nano.Web.Api.Requests;

namespace Nano.Template.Web.Models.Api.Requests;

/// <summary>
/// Samples Custom Request.
/// </summary>
public class SamplesCustomRequest : BaseRequestPost
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public SamplesCustomRequest()
    {
        this.Controller = "samples";
        this.Action = "custom";
    }

    /// <inheritdoc />
    public override object GetBody()
    {
        return null;
    }
}