using System;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;

namespace Wjire.Db.Dapper.SqlServer.Test
{
    public class Consumer : ICapSubscribe
    {
        private readonly ILogger<Consumer> _logger;

        public Consumer(ILogger<Consumer> logger)
        {
            _logger = logger;
        }


        [CapSubscribe("test.company.add", Group = "consumer1")]
        public void ReceiveMessage1(DateTime time)
        {
            _logger.LogInformation("1 message time is:" + time);
        }

        [CapSubscribe("test.company.add", Group = "consumer2")]
        public void ReceiveMessage2(DateTime time)
        {
            _logger.LogInformation("2 message time is:" + time);
        }

    }
}
