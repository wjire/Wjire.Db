using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Wjire.Db.Dapper.SqlServer.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<Consumer>();
                    services.AddSingleton<SqlServerConnectionFactoryProvider>();
                    services.AddCap(x =>
                    {
                        x.UseRabbitMQ(configure =>
                        {
                            configure.HostName = "127.0.0.1"; //设置服务器地址
                            configure.Port = 5672; //设置端口号
                            configure.VirtualHost = "/vhost_wjire"; //设置虚拟主机
                            configure.UserName = "wjire"; //设置用户名
                            configure.Password = "Aa1111"; //设置密码
                        });
                        x.UseSqlServer(s =>
                        {
                            s.ConnectionString =
                               "Data Source=localhost;Initial Catalog=nCoV;Persist Security Info=True;User ID=sa;Password=Aa1111";
                        });
                    });
                });
        }
    }
}
