using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wjire.Dapper;
using Wjire.Dapper.SqlServer;

namespace Wjire.Db.Dapper.SqlServer.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<CompanyLogic>();
            services.AddDapper(x =>
            {
                //x.UseCapDbContext();
                x.UseSqlServer(configure =>
                {
                    configure.Read = _configuration.GetSection("connectionStrings")["Read"];
                    configure.Write = _configuration.GetSection("connectionStrings")["Write"];
                });
            });

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
