using Nano.Data;
using Nano.Data.Providers.MySql;
//using Nano.Data.Providers.SqlServer;

namespace Nano.Template.Web.Data
{
    /// <inheritdoc />
    public class WebDbContextFactory : BaseDbContextFactory</*SqlServerProvider*/MySqlProvider, WebDbContext>
    {

    }
}