using Nano.Data;
using Nano.Data.Providers.MySql;

namespace Nano.Template.Data
{
    /// <inheritdoc />
    public class WebDbContextFactory : BaseDbContextFactory<MySqlProvider, WebDbContext>
    {

    }
}
