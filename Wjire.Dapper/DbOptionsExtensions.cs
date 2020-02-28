namespace Wjire.Dapper
{
    public static class DbOptionsExtensions
    {
        public static DbOptions UseDbContext(this DbOptions options)
        {
            options.RegisterExtension(new DbContextDbOptionsExtension());
            return options;
        }
    }
}
