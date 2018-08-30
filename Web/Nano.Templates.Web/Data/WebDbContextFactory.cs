using Nano.Data;
using Nano.Data.Providers.MySql;

namespace Nano.Templates.Web.Data
{
    /// <inheritdoc />
    public class WebDbContextFactory : BaseDbContextFactory<MySqlProvider, WebDbContext>
    {

    }
}