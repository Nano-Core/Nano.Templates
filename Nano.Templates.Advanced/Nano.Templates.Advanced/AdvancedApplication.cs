using Microsoft.Extensions.Configuration;
using Nano.App;

namespace Nano.Templates.Advanced
{
    /// <inheritdoc />
    public class AdvancedApplication : DefaultApplication
    {
        /// <inheritdoc />
        public AdvancedApplication(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}