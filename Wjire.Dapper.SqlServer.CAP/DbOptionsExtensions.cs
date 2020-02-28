namespace Wjire.Dapper.SqlServer.CAP
{

    public static class DbOptionsExtensions
    {
        public static DbOptions UseCapDbContext(this DbOptions options)
        {
            options.RegisterExtension(new CapDbContextDbOptionsExtension());
            return options;
        }
    }
}
