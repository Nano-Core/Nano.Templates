using Microsoft.Extensions.Configuration;
using Nano.App;

namespace Nano.Templates.Spatial
{
    /// <inheritdoc />
    public class SpatialApplication : DefaultApplication
    {
        /// <inheritdoc />
        public SpatialApplication(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}