using System;

namespace Wjire.Dapper.SqlServer
{

    public static class DbOptionsExtensions
    {
        public static DbOptions UseSqlServer(this DbOptions options, Action<ConnectionOptions> connectionConfigure)
        {
            options.RegisterExtension(new SqlServerDbOptionsExtension(connectionConfigure));
            return options;
        }
    }
}
