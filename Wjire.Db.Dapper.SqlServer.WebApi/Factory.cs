using Microsoft.Extensions.Options;
using Wjire.Dapper;

namespace Wjire.Db.Dapper.SqlServer.WebApi
{
    public static partial class Factory
    {
        private static readonly string Read;
        private static readonly string Write;

        static Factory()
        {
            ConnectionOptions connectionOptions = ServiceCollectionExtensions.GetRequiredService<IOptions<ConnectionOptions>>().Value;
            Read = connectionOptions.Read;
            Write = connectionOptions.Write;
        }
    }
}
