using Microsoft.Extensions.Configuration;
using Nano.App;

namespace Nano.Templates.Simple
{
    /// <inheritdoc />
    public class SimpleApplication : DefaultApplication
    {
        /// <inheritdoc />
        public SimpleApplication(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}